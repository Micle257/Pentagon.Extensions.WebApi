// -----------------------------------------------------------------------
//  <copyright file="RequestHandler.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.WebApi
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading;
    using System.Threading.Tasks;
    using Interfaces;
    using IO.Json;
    using JetBrains.Annotations;
    using Requests;
    using Responses;

    public abstract class RequestHandler : IRequestHandler
    {
        HttpClient _httpClient;

        public async Task<TResponse> ExecuteSingleRequest<TResponse,T>(IRequest<T> request, CancellationToken cancellationToken = default)
                where TResponse : IResponse<T>, new()
        {
            Require.ValidateRequest(request);

            SetupHttpClient();

            var msg = BuildMessage(request);

            var result = await QueryAsync(msg,
                                          (message, s) => ApiResponseFactory<TResponse, T>(s),
                                          cancellationToken).ConfigureAwait(false);

            return result;
        }

        /// <inheritdoc />
        public async Task<TResponse> ExecuteManyRequest<TResponse, T>(IRequest<T> request, CancellationToken cancellationToken = default)
                where TResponse : IListResponse<T>, new()
        {
            Require.ValidateRequest(request);

            SetupHttpClient();

            var msg = BuildMessage(request);

            var result = await QueryAsync(msg,
                                          (message, s) => ApiResponseFactory<TResponse, IEnumerable<T>>(s),
                                          cancellationToken).ConfigureAwait(false);

            return result;
        }

        /// <summary> Sets the default request headers for <see cref="HttpClient" /> which defines content type, client id and api version headers. </summary>
        /// <param name="httpClient"> The HTTP client. </param>
        protected virtual void SetDefaultRequestHeaders(HttpClient httpClient)
        {
            var appHeader = new MediaTypeWithQualityHeaderValue(ContentTypeNames.Json);

            if (!httpClient.DefaultRequestHeaders.Accept.Contains(appHeader))
                httpClient.DefaultRequestHeaders.Accept.Add(appHeader);
        }

        protected virtual ApiException HandleResponseStatusCode(IBasicResponse responseMessage, IRequestMessage requestMessage) => null;

        protected virtual void OnResponseCreated(IBasicResponse response) { }

        protected abstract IRequestMessage BuildMessage(IRequest request);

        static TResponse ApiResponseFactory<TResponse, T>(string content)
            where TResponse : IResponse<T>
        {
            var objectContent = JsonHelpers.Deserialize<T>(content);
            var hasValue = !Equals(objectContent, default(T));

            var response = Activator.CreateInstance<TResponse>();

            response.HasValue = hasValue;
            response.Value = objectContent;

            return response;
        }

        async Task<TResponse> QueryAsync<TResponse>([NotNull] IRequestMessage requestMessage,
                                                         [NotNull] Func<HttpResponseMessage, string, TResponse> res,
                                                         CancellationToken cancellationToken = default)
                where TResponse : IBasicResponse
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
                response.Headers = new ApiResponseHeaders
                                   {
                                           BaseHeaders = responseMessage.Headers
                                   };

                var error = HandleResponseStatusCode(response, requestMessage);

                if (error != null)
                {
                    response.IsSuccessful = false;
                    response.Exception = error;
                }
                else
                    response.IsSuccessful = true;

                OnResponseCreated(response);

                return response;
            }
            catch (Exception exception)
            {
                var response = new NoContentResponse
                               {
                                       IsSuccessful = false
                               };

                response.Exception = new ApiException(new ApiExceptionArguments(requestMessage, response), message: "Error while executing request.", exception);

                if (responseMessage != null)
                {
                    response.ReasonPhrase = responseMessage.ReasonPhrase;
                    response.StatusCode = responseMessage.StatusCode;
                    response.Headers = new ApiResponseHeaders
                                       {
                                               BaseHeaders = responseMessage.Headers
                                       };

                    OnResponseCreated(response);
                }

                return (TResponse) (IBasicResponse) response;
            }
            finally
            {
                responseMessage?.Dispose();
            }
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