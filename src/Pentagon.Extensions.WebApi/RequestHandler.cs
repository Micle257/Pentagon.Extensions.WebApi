// -----------------------------------------------------------------------
//  <copyright file="RequestHandler.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.WebApi
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading;
    using System.Threading.Tasks;
    using Abstractions;
    using Interfaces;
    using IO.Json;
    using JetBrains.Annotations;
    using Requests;
    using Responses;
    
    public abstract class RequestHandler : IRequestHandler
    {
        HttpClient _httpClient;

        /// <summary> Executes the single item request with no data sending. </summary>
        /// <typeparam name="T"> Type of content object. </typeparam>
        /// <param name="request"> The execute request. </param>
        /// <param name="cancellationToken"> The <see cref="CancellationToken" /> to observe while waiting for the task to complete. </param>
        /// <returns> <see cref="TraktResponse{TContent}" /> instance with response information. </returns>
        public Task<IResponse<T, THeaders>> ExecuteSingleItemRequest<T, THeaders>(IRequest<T> request, CancellationToken cancellationToken = default)
                where THeaders : IApiResponseHeaders
        {
            Require.ValidateRequest(request);

            SetupHttpClient();

            var msg = BuildMessage(request);

            return QuerySingleItem<T, THeaders>(msg, cancellationToken);
        }

        public Task<IListResponse<TContent, THeaders>> ExecuteListRequestAsync<TContent, THeaders>(IRequest<TContent> request, CancellationToken cancellationToken = default)
                where THeaders : IApiResponseHeaders
        {
            Require.ValidateRequest(request);

            SetupHttpClient();

            var msg = BuildMessage(request);

            return QueryList<TContent, THeaders>(msg, cancellationToken);
        }

        public Task<IPagedResponse<TContent, THeaders>> ExecutePagedRequest<TContent, THeaders>(IRequest<TContent> request, CancellationToken cancellationToken = default)
                where THeaders : IApiResponseHeaders
        {
            Require.ValidateRequest(request);

            SetupHttpClient();

            var msg = BuildMessage(request);

            return QueryPagedList<TContent, THeaders>(msg, cancellationToken);
        }
        
        /// <summary> Sets the default request headers for <see cref="HttpClient" /> which defines content type, client id and api version headers. </summary>
        /// <param name="httpClient"> The HTTP client. </param>
        protected virtual void SetDefaultRequestHeaders(HttpClient httpClient)
        {
            var appHeader = new MediaTypeWithQualityHeaderValue(ContentTypeNames.Json);

            if (!httpClient.DefaultRequestHeaders.Accept.Contains(appHeader))
                httpClient.DefaultRequestHeaders.Accept.Add(appHeader);
        }

        protected abstract IRequestMessage BuildMessage(IRequest request);
        
        async Task<IHeadResponse<THeaders>> QueryAsync<TResponse, THeaders>([NotNull] IRequestMessage requestMessage,
                                                    [NotNull] Func<HttpResponseMessage, string ,TResponse> res,
                                                    CancellationToken cancellationToken = default)
            where TResponse : IHeadResponse<THeaders>
            where THeaders : IApiResponseHeaders
        {
            if (requestMessage == null)
                throw new ArgumentNullException(nameof(requestMessage));

            if (res == null)
                throw new ArgumentNullException(nameof(res));

            var responseMessage = default(HttpResponseMessage);
            
            try
            {
                responseMessage = await ExecuteRequest(requestMessage, cancellationToken).ConfigureAwait(false);

                var responseContent = await GetResponseContent(responseMessage).ConfigureAwait(false);

                var response = res(responseMessage, responseContent);
                
                response.ReasonPhrase = responseMessage.ReasonPhrase;
                response.StatusCode = responseMessage.StatusCode;
                response.Content = responseContent;
                response.Headers = BuildHeaders<THeaders>(responseMessage.Headers);

                var error = HandleResponseStatusCode(response, requestMessage);

                if (error != null)
                {
                    response.IsSuccessful = false;
                    response.Exception = error;
                }

                response.IsSuccessful = true;

                return response;
            }
            catch (Exception exception)
            {
                var response = new NoContentResponse<THeaders>
                               {
                                       IsSuccessful = false
                };

                response.Exception = new ApiException(new ApiExceptionArguments(requestMessage, response), message: "Error while executing request.", exception);

                if (responseMessage != null)
                {
                    response.ReasonPhrase = responseMessage.ReasonPhrase;
                    response.StatusCode = responseMessage.StatusCode;
                    response.Headers = BuildHeaders<THeaders>(responseMessage.Headers);
                }
                
                return response;
            }
            finally
            {
                responseMessage?.Dispose();
            }
        }

        protected virtual ApiException HandleResponseStatusCode<THeaders>(IHeadResponse<THeaders> responseMessage, IRequestMessage requestMessage)
                where THeaders : IApiResponseHeaders => null;
        
        THeaders BuildHeaders<THeaders>(HttpResponseHeaders headers)
                where THeaders : IApiResponseHeaders
        {
            var head = BuildHeadersCore<THeaders>(headers);

            return head;
        }

        protected abstract THeaders BuildHeadersCore<THeaders>(HttpResponseHeaders httpHeaders)
                where THeaders : IApiResponseHeaders;

        /// <summary> Executes the query for single item with <see cref="TraktRequestMessage" />. </summary>
        /// <typeparam name="T"> Type of content object. </typeparam>
        /// <param name="requestMessage"> The request message. </param>
        /// <param name="isCheckinRequest"> The value indicates if checkin is requested. </param>
        /// <param name="cancellationToken"> The <see cref="CancellationToken" /> to observe while waiting for the task to complete. </param>
        /// <exception cref="System.ArgumentException"> Single item must return content. </exception>
        async Task<IResponse<T, THeaders>> QuerySingleItem<T, THeaders>(IRequestMessage requestMessage, CancellationToken cancellationToken = default)
                where THeaders : IApiResponseHeaders
        {
            var result = await QueryAsync<IResponse<T, THeaders>, THeaders>(requestMessage,
                             (message, content) =>
                             {
                                 var objectContent = JsonHelpers.Deserialize<T>(content);
                                 var hasValue = !Equals(objectContent, default(T));

                                 var response = new ApiResponse<T, THeaders>
                                                {
                                                        HasValue = hasValue,
                                                        Value = objectContent
                                                };

                                 return response;
                             },
                             cancellationToken);

            if (result is IResponse<T, THeaders> res)
                return res;

            return new ApiResponse<T, THeaders>(result);
        }

        async Task<IListResponse<TContent, THeaders>> QueryList<TContent, THeaders>(IRequestMessage requestMessage, CancellationToken cancellationToken = default)
                where THeaders : IApiResponseHeaders
        {
            var result = await QueryAsync<IListResponse<TContent, THeaders>, THeaders>(requestMessage,
                                          (message, content) =>
                                          {
                                              var contentObject = JsonHelpers.Deserialize<IEnumerable<TContent>>(content);

                                              var response = new ListApiResponse<TContent, THeaders>
                                                             {
                                                                     HasValue = contentObject != null,
                                                                     Value = contentObject
                                                             };

                                              return response;
                                          },
                                          cancellationToken);

            if (result is IListResponse<TContent, THeaders> res)
                return res;

            return new ListApiResponse<TContent, THeaders>(result);
        }

        async Task<IPagedResponse<TContent, THeaders>> QueryPagedList<TContent, THeaders>(IRequestMessage requestMessage, CancellationToken cancellationToken = default)
                where THeaders : IApiResponseHeaders
        {
            var result = await QueryAsync<IPagedResponse<TContent, THeaders>, THeaders>(requestMessage,
                                          (message, content) =>
                                          {
                                              var contentObject = JsonHelpers.Deserialize<IEnumerable<TContent>>(content);

                                              var response = new PagedApiResponse<TContent, THeaders>
                                                             {
                                                                     HasValue = contentObject != null,
                                                                     Value = contentObject
                                                             };

                                              return response;
                                          },
                                          cancellationToken);

            if (result is IPagedResponse<TContent, THeaders> res)
                return res;

            return new PagedApiResponse<TContent, THeaders>(result);
        }

        /// <summary> Gets the string content of the response. </summary>
        /// <param name="responseMessage"> The response message. </param>
        /// <param name="cancellationToken"> The <see cref="CancellationToken" /> to observe while waiting for the task to complete. </param>
        /// <returns> Awaitable string value of content. </returns>
        Task<string> GetResponseContent(HttpResponseMessage responseMessage)
            => responseMessage.Content != null ? responseMessage.Content.ReadAsStringAsync() : Task.FromResult(string.Empty);

        /// <summary> Executes the request and sends it via <see cref="HttpClient" />. </summary>
        /// <param name="requestMessage"> The request message to send. </param>
        /// <param name="isCheckinRequest"> The value indicates if checkin is requested. </param>
        /// <param name="cancellationToken"> The <see cref="CancellationToken" /> to observe while waiting for the task to complete. </param>
        /// <returns> <see cref="HttpRequestMessage" /> which contains the response data. </returns>
        async Task<HttpResponseMessage> ExecuteRequest(IRequestMessage requestMessage, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.SendAsync((RequestMessage) requestMessage, cancellationToken).ConfigureAwait(false);

            return response;
        }

        /// <summary> Initializes and configures the HTTP client. </summary>
        void SetupHttpClient()
        {
            if (_httpClient == null)
                _httpClient = new HttpClient();

            SetDefaultRequestHeaders(_httpClient);
        }

        protected static class Require
        {
            public static void ValidateRequest(IRequest request)
            {
                if (request is null)
                    throw new ArgumentNullException(nameof(request));

                var validate = request.Validate();

                if (!validate.IsValid)
                {
                    var ex = new AggregateException(validate.Errors);
                    throw ex;
                }
            }
        }
    }
}