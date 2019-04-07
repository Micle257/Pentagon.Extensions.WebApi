// -----------------------------------------------------------------------
//  <copyright file="RequestHandler.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.WebApi
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
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

    public class RequestHandler : IRequestHandler
    {
        readonly IApiConfiguration _configuration;
        readonly IRequestMessageBuilder _builder;

        HttpClient _httpClient;

        public RequestHandler(IApiConfiguration configuration)
        {
            _configuration = configuration;
            _builder = new RequestMessageBuilder(configuration);
        }

        /// <summary> Executes the single item request with no data sending. </summary>
        /// <typeparam name="T"> Type of content object. </typeparam>
        /// <param name="request"> The execute request. </param>
        /// <param name="cancellationToken"> The <see cref="CancellationToken" /> to observe while waiting for the task to complete. </param>
        /// <returns> <see cref="TraktResponse{TContent}" /> instance with response information. </returns>
        public Task<IResponse<T>> ExecuteSingleItemRequest<T>(IRequest<T> request, CancellationToken cancellationToken = default)
        {
            Require.ValidateRequest(request);
            SetupHttpClient();

            var msg = _builder.AddRequest(request).Build();

            return QuerySingleItem<T>(msg, cancellationToken);
        }

        /// <inheritdoc />
        public Task<IResponse<TContent>> ExecuteSingleItemPostRequest<TContent, TRequestBody, TRequest>(TRequest postRequest)
                where TRequestBody : class
                where TRequest : IHasRequestBody<TRequestBody>, IRequest<TContent>
        {
            Require.ValidateRequest(postRequest);
            SetupHttpClient();

            var msg = _builder.AddPostRequest(postRequest).Build();

            return QuerySingleItem<TContent>(msg);
        }
        
        public Task<IListResponse<TContent>> ExecuteListRequestAsync<TContent>(IRequest<TContent> request)
        {
            Require.ValidateRequest(request);
            SetupHttpClient();
            var msg = _builder.AddRequest(request).Build();
            return QueryList<TContent>(msg);
        }

        public Task<IPagedResponse<TContent>> ExecutePagedRequest<TContent, TRequest>(TRequest request)
                where TRequest : IRequest<TContent>, ISupportsPagination
        {
            Require.ValidateRequest(request);
            SetupHttpClient();

            var msg = _builder.AddRequest(request).Build();

            return QueryPagedList<TContent>(msg);
        }

        public async Task<IPagedResponse<TContent>> ExecuteOnePagedRequest<TContent, TRequest>(TRequest request)
                where TRequest : IRequest<TContent>, ISupportsPagination
        {
            var response = await ExecutePagedRequest<TContent, TRequest>(request).ConfigureAwait(false);

            if (response.IsSuccess && response.HasValue && response.ItemCount.HasValue)
            {
                request.Limit = response.ItemCount.Value;
                response = await ExecutePagedRequest<TContent, TRequest>(request).ConfigureAwait(false);
            }

            return response;
        }

        /// <summary> Executes the query for single item with <see cref="TraktRequestMessage" />. </summary>
        /// <typeparam name="T"> Type of content object. </typeparam>
        /// <param name="requestMessage"> The request message. </param>
        /// <param name="isCheckinRequest"> The value indicates if checkin is requested. </param>
        /// <param name="cancellationToken"> The <see cref="CancellationToken" /> to observe while waiting for the task to complete. </param>
        /// <returns> <see cref="TraktResponse{TContent}" /> instance. </returns>
        /// <exception cref="System.ArgumentException"> Single item must return content. </exception>
        async Task<IResponse<T>> QuerySingleItem<T>(IRequestMessage requestMessage, CancellationToken cancellationToken = default)
        {
            var responseMessage = default(HttpResponseMessage);
            var url = requestMessage.Url;
            var body = requestMessage.RequestBodyJson;

            try
            {
                responseMessage = await ExecuteRequest(requestMessage, cancellationToken).ConfigureAwait(false);

                var responseContent = await GetResponseContent(responseMessage).ConfigureAwait(false);

                if (!responseMessage.IsSuccessStatusCode) { }

                var objectContent = JsonHelpers.Deserialize<T>(responseContent);
                var hasValue = !Equals(objectContent, default(T));

                var response = new ApiResponse<T>
                               {
                                       IsSuccess = true,
                                       HasValue = hasValue,
                                       Value = objectContent,
                                       RawContent = responseContent,
                                       StatusCode = responseMessage.StatusCode
                               };

                if (response.StatusCode == HttpStatusCode.NoContent)
                {
                    response.IsSuccess = false;
                    response.Exception = new ApiException(new ApiExceptionArguments(url, body, response),
                                                          message: "Single item must return content.",
                                                          exception: new ArgumentException(message: "Single item must return content."));
                }

                return response;
            }
            catch (Exception exception)
            {
                var response = new ApiResponse<T>
                               {
                                       IsSuccess = false
                               };

                response.Exception = new ApiException(new ApiExceptionArguments(url, body, response), message: "Error while executing request.", exception: exception);

                return response;
            }
            finally
            {
                responseMessage?.Dispose();
            }
        }

        async Task<IListResponse<TContent>> QueryList<TContent>(IRequestMessage requestMessage)
        {
            var responseMessage = default(HttpResponseMessage);
            var url = requestMessage.Url;
            var body = requestMessage.RequestBodyJson;

            try
            {
                responseMessage = await ExecuteRequest(requestMessage).ConfigureAwait(false);

                var responseContent = await GetResponseContent(responseMessage).ConfigureAwait(false);
                var contentObject = JsonHelpers.Deserialize<IEnumerable<TContent>>(responseContent);

                var response = new ListApiResponse<TContent>
                               {
                                       IsSuccess = true,
                                       HasValue = contentObject != null,
                                       Value = contentObject,
                                       RawContent = responseContent,
                                       StatusCode = responseMessage.StatusCode
                               };

                return response;
            }
            catch (Exception e)
            {
                var response = new ListApiResponse<TContent>
                               {
                                       IsSuccess = false
                               };

                response.Exception = new ApiException(new ApiExceptionArguments(url, body, response), message: "Error while executing request.", exception: e);

                return response;
            }
            finally
            {
                responseMessage?.Dispose();
            }
        }

        async Task<IPagedResponse<TContent>> QueryPagedList<TContent>(IRequestMessage requestMessage)
        {
            var responseMessage = default(HttpResponseMessage);
            var url = requestMessage.Url;
            var body = requestMessage.RequestBodyJson;

            try
            {
                responseMessage = await ExecuteRequest(requestMessage).ConfigureAwait(false);

                var responseContent = await GetResponseContent(responseMessage).ConfigureAwait(false);
                var contentObject = JsonHelpers.Deserialize<IEnumerable<TContent>>(responseContent);

                var response = new PagedApiResponse<TContent>
                               {
                                       IsSuccess = true,
                                       HasValue = contentObject != null,
                                       Value = contentObject,
                                       RawContent = responseContent,
                                       StatusCode = responseMessage.StatusCode
                               };

                if (responseMessage.Headers != null)
                {
                    ParseSortingResponseHeaders(response, responseMessage.Headers);
                    ParsePagedResponseHeaderValues(response, responseMessage.Headers);
                }

                return response;
            }
            catch (Exception e)
            {
                var response = new PagedApiResponse<TContent>
                               {
                                       IsSuccess = false
                               };

                response.Exception = new ApiException(new ApiExceptionArguments(url, body, response), message: "Error while executing request.", exception: e);
                ;

                return response;
            }
            finally
            {
                responseMessage?.Dispose();
            }
        }

        void ParsePagedResponseHeaderValues(IPagedResponseHeaders pagedResponseHeaders, HttpResponseHeaders responseHeaders)
        {
            IEnumerable<string> values;

            if (responseHeaders.TryGetValues(ApiHeaderNames.PaginationHeaders.Page, out values))
            {
                var pageValue = values.First();
                if (int.TryParse(pageValue, out var page))
                    pagedResponseHeaders.Page = page;
            }

            if (responseHeaders.TryGetValues(ApiHeaderNames.PaginationHeaders.Limit, out values))
            {
                var limitValue = values.First();
                if (int.TryParse(limitValue, out var limit))
                    pagedResponseHeaders.Limit = limit;
            }

            if (responseHeaders.TryGetValues(ApiHeaderNames.PaginationHeaders.PageCount, out values))
            {
                var count = values.First();

                if (int.TryParse(count, out var pageCount))
                    pagedResponseHeaders.PageCount = pageCount;
            }

            if (responseHeaders.TryGetValues(ApiHeaderNames.PaginationHeaders.ItemCount, out values))
            {
                var count = values.First();

                if (int.TryParse(count, out var itemCount))
                    pagedResponseHeaders.ItemCount = itemCount;
            }
        }

        void ParseSortingResponseHeaders(ISortingResponseHeaders headerResults, HttpResponseHeaders responseHeaders)
        {
            IEnumerable<string> values;

            if (responseHeaders.TryGetValues(ApiHeaderNames.SortHeaders.SortBy, out values))
                headerResults.SortBy = values.First();

            if (responseHeaders.TryGetValues(ApiHeaderNames.SortHeaders.SortHow, out values))
                headerResults.SortHow = values.First();
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

        /// <summary> Sets the default request headers for <see cref="HttpClient" /> which defines content type, client id and api version headers. </summary>
        /// <param name="httpClient"> The HTTP client. </param>
        void SetDefaultRequestHeaders(HttpClient httpClient)
        {
            var appHeader = new MediaTypeWithQualityHeaderValue(ContentTypeNames.Json);

            if (!httpClient.DefaultRequestHeaders.Contains(ApiHeaderNames.TrustedClientId))
                httpClient.DefaultRequestHeaders.Add(ApiHeaderNames.TrustedClientId, _configuration.ClientId);

            if (!httpClient.DefaultRequestHeaders.Accept.Contains(appHeader))
                httpClient.DefaultRequestHeaders.Accept.Add(appHeader);
        }

        static class Require
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