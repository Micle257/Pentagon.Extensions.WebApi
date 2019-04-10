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
        public Task<IResponse<T>> ExecuteSingleItemRequest<T>(IRequest<T> request, CancellationToken cancellationToken = default)
        {
            Require.ValidateRequest(request);

            SetupHttpClient();

            var msg = BuildMessage(request);

            return QuerySingleItem<T>(msg, cancellationToken);
        }

        public Task<IListResponse<TContent>> ExecuteListRequestAsync<TContent>(IRequest<TContent> request, CancellationToken cancellationToken = default)
        {
            Require.ValidateRequest(request);

            SetupHttpClient();

            var msg = BuildMessage(request);

            return QueryList<TContent>(msg, cancellationToken);
        }

        public Task<IPagedResponse<TContent>> ExecutePagedRequest<TContent, TRequest>(TRequest request, CancellationToken cancellationToken = default)
                where TRequest : IRequest<TContent>, ISupportsPagination
        {
            Require.ValidateRequest(request);

            SetupHttpClient();

            var msg = BuildMessage(request);

            return QueryPagedList<TContent>(msg, cancellationToken);
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
        
        async Task<IResponse> QueryAsync<TResponse>([NotNull] IRequestMessage requestMessage,
                                                    [NotNull] Func<HttpResponseMessage, string ,TResponse> res,
                                                    CancellationToken cancellationToken = default)
            where TResponse : IResponse
        {
            if (requestMessage == null)
                throw new ArgumentNullException(nameof(requestMessage));

            if (res == null)
                throw new ArgumentNullException(nameof(res));

            var responseMessage = default(HttpResponseMessage);

            var url = requestMessage.Url;
            var body = requestMessage.RequestBodyJson;

            try
            {
                responseMessage = await ExecuteRequest(requestMessage, cancellationToken).ConfigureAwait(false);

                var responseContent = await GetResponseContent(responseMessage).ConfigureAwait(false);

                var error = HandleResponseStatusCode(responseMessage, requestMessage);

                var response = res(responseMessage, responseContent);
                
                if (error != null)
                {
                    response.IsSuccess = false;
                    response.Exception = error;
                }

                response = HandleResponseHeaders(responseMessage.Headers, response);

                response.IsSuccess = true;
                response.Headers = responseMessage.Headers;
                response.ReasonPhrase = responseMessage.ReasonPhrase;
                response.StatusCode = responseMessage.StatusCode;

                return response;
            }
            catch (Exception exception)
            {
                var response = new NoContentResponse
                               {
                                       Exception = new ApiException(new ApiExceptionArguments(url, body, null), message: "Error while executing request.", exception),
                                       IsSuccess = false
                };

                if (responseMessage != null)
                {
                    response.Headers = responseMessage.Headers;
                    response.ReasonPhrase = responseMessage.ReasonPhrase;
                    response.StatusCode = responseMessage.StatusCode;
                }
                
                return response;
            }
            finally
            {
                responseMessage?.Dispose();
            }
        }

        protected virtual ApiException HandleResponseStatusCode(HttpResponseMessage responseMessage, IRequestMessage requestMessage)
        {


            return null;
        }

        protected virtual TResponse HandleResponseHeaders<TResponse>(HttpResponseHeaders headers, TResponse response)
                where TResponse : IResponse =>
                response;

        /// <summary> Executes the query for single item with <see cref="TraktRequestMessage" />. </summary>
        /// <typeparam name="T"> Type of content object. </typeparam>
        /// <param name="requestMessage"> The request message. </param>
        /// <param name="isCheckinRequest"> The value indicates if checkin is requested. </param>
        /// <param name="cancellationToken"> The <see cref="CancellationToken" /> to observe while waiting for the task to complete. </param>
        /// <exception cref="System.ArgumentException"> Single item must return content. </exception>
        async Task<IResponse<T>> QuerySingleItem<T>(IRequestMessage requestMessage, CancellationToken cancellationToken = default)
        {
            var result = await QueryAsync(requestMessage,
                             (message, content) =>
                             {
                                 var objectContent = JsonHelpers.Deserialize<T>(content);
                                 var hasValue = !Equals(objectContent, default(T));

                                 var response = new ApiResponse<T>
                                                {
                                                        HasValue = hasValue,
                                                        Value = objectContent,
                                                        RawContent = content
                                                };

                                 return response;
                             },
                             cancellationToken);

            if (result is IResponse<T> res)
                return res;

            return new ApiResponse<T>(result);
        }

        async Task<IListResponse<TContent>> QueryList<TContent>(IRequestMessage requestMessage, CancellationToken cancellationToken = default)
        {
            var result = await QueryAsync(requestMessage,
                                          (message, content) =>
                                          {
                                              var contentObject = JsonHelpers.Deserialize<IEnumerable<TContent>>(content);

                                              var response = new ListApiResponse<TContent>
                                                             {
                                                                     HasValue = contentObject != null,
                                                                     Value = contentObject,
                                                                     RawContent = content
                                                             };

                                              return response;
                                          },
                                          cancellationToken);

            if (result is IListResponse<TContent> res)
                return res;

            return new ListApiResponse<TContent>(result);
        }

        async Task<IPagedResponse<TContent>> QueryPagedList<TContent>(IRequestMessage requestMessage, CancellationToken cancellationToken = default)
        {
            var result = await QueryAsync(requestMessage,
                                          (message, content) =>
                                          {
                                              var contentObject = JsonHelpers.Deserialize<IEnumerable<TContent>>(content);

                                              var response = new PagedApiResponse<TContent>
                                                             {
                                                                     HasValue = contentObject != null,
                                                                     Value = contentObject,
                                                                     RawContent = content
                                                             };

                                              return response;
                                          },
                                          cancellationToken);

            if (result is IPagedResponse<TContent> res)
                return res;

            return new PagedApiResponse<TContent>(result);
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