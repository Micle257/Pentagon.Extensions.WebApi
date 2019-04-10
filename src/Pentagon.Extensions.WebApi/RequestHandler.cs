﻿// -----------------------------------------------------------------------
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
    using Requests;
    using Responses;

    public class RequestExecuteResult<TResponse>
        where TResponse : IResponse
    {
        public bool IsSuccessful { get; set; }

        public Exception Exception { get; set; }

        public TResponse Response { get; set; }
    }

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
        
        async Task<RequestExecuteResult<TResponse>> QueryAsync<TResponse>(IRequestMessage requestMessage, Func<HttpResponseMessage, string ,TResponse> res, CancellationToken cancellationToken = default)
            where TResponse : IResponse
        {
            var responseMessage = default(HttpResponseMessage);
            var url = requestMessage.Url;
            var body = requestMessage.RequestBodyJson;

            try
            {
                responseMessage = await ExecuteRequest(requestMessage, cancellationToken).ConfigureAwait(false);

                var responseContent = await GetResponseContent(responseMessage).ConfigureAwait(false);

                var error = HandleResponseStatusCode(responseMessage, requestMessage);

                var response = res(responseMessage, responseContent);

                //if (response.StatusCode == HttpStatusCode.NoContent)
                //{
                //    response.IsSuccess = false;
                //    response.Exception = new ApiException(new ApiExceptionArguments(url, body, response),
                //                                          message: "Single item must return content.",
                //                                          new ArgumentException(message: "Single item must return content."));
                //}


                if (error != null)
                {
                    return new RequestExecuteResult<TResponse>
                           {
                                IsSuccessful = false,
                                Exception = error,
                                Response = response
                           };
                }

                response = HandleResponseHeaders(responseMessage.Headers, response);

                return new RequestExecuteResult<TResponse>
                {
                               IsSuccessful = true,
                               Response = response
                       };
            }
            catch (Exception exception)
            {
                var response = new RequestExecuteResult<TResponse>
                {
                    IsSuccessful = false,
                    Exception = new ApiException(new ApiExceptionArguments(url, body, null), message: "Error while executing request.", exception)
            };
                
                return response;
            }
            finally
            {
                responseMessage?.Dispose();
            }
        }

        protected virtual Exception HandleResponseStatusCode(HttpResponseMessage responseMessage, IRequestMessage requestMessage)
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
                                                        IsSuccess = true,
                                                        HasValue = hasValue,
                                                        Value = objectContent,
                                                        RawContent = content,
                                                        StatusCode = message.StatusCode,
                                                        Headers = message.Headers
                                                };

                                 return response;
                             },
                             cancellationToken);

            return result.Response;
        }

        async Task<IListResponse<TContent>> QueryList<TContent>(IRequestMessage requestMessage, CancellationToken cancellationToken = default)
        {
            var result = await QueryAsync(requestMessage,
                                          (message, content) =>
                                          {
                                              var contentObject = JsonHelpers.Deserialize<IEnumerable<TContent>>(content);

                                              var response = new ListApiResponse<TContent>
                                                             {
                                                                     IsSuccess = true,
                                                                     HasValue = contentObject != null,
                                                                     Value = contentObject,
                                                                     RawContent = content,
                                                                     StatusCode = message.StatusCode,
                                                                     Headers = message.Headers
                                                             };

                                              return response;
                                          },
                                          cancellationToken);

            return result.Response;
        }

        async Task<IPagedResponse<TContent>> QueryPagedList<TContent>(IRequestMessage requestMessage, CancellationToken cancellationToken = default)
        {
            var result = await QueryAsync(requestMessage,
                                          (message, content) =>
                                          {
                                              var contentObject = JsonHelpers.Deserialize<IEnumerable<TContent>>(content);

                                              var response = new PagedApiResponse<TContent>
                                                             {
                                                                     IsSuccess = true,
                                                                     HasValue = contentObject != null,
                                                                     Value = contentObject,
                                                                     RawContent = content,
                                                                     StatusCode = message.StatusCode,
                                                                     Headers = message.Headers
                                                             };

                                              return response;
                                          },
                                          cancellationToken);

            return result.Response;
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