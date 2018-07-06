// -----------------------------------------------------------------------
//  <copyright file="RequestMessageBuilder.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.WebApi
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using Abstractions;
    using Interfaces;
    using Requests;
    using Tavis.UriTemplates;
    using Utilities.Data.Json;

    public class RequestMessageBuilder : IRequestMessageBuilder
    {
        readonly IApiConfiguration _configuration;

        object _requestBody;

        public RequestMessageBuilder(IApiConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected IRequest Request { get; private set; }

        public IRequestMessageBuilder AddRequest(IRequest request)
        {
            Request = request;
            return this;
        }

        public IRequestMessageBuilder AddPostRequest<TRequestBody>(IHasRequestBody<TRequestBody> request)
        {
            Request = request as IRequest;
            _requestBody = request.RequestBody;
            return this;
        }

        public IRequestMessage Build()
        {
            var url = BuildUrl();

            var request = new RequestMessage(Request.Method, url) {Url = url.AbsoluteUri};

            SetAuthorizationHeaders(request);

            if (_requestBody != null)
            {
                var content = GetRequestBodyContent();
                request.Content = content.httpContent;
                request.RequestBodyJson = content.jsonContent;
            }
            
            return request;
        }
        
        /// <summary> Sets the request message headers for authorization by given <see cref="AuthorizationRequirement" />. </summary>
        protected virtual void SetAuthorizationHeaders(IRequestMessage request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(RequestMessage));

            if (Request is ITrustedRequest)
            {
                if (string.IsNullOrEmpty(_configuration.ClientId))
                    throw new ArgumentException(message: "Client Id is missing and it's required for this request.");

                request.Headers.Add(ApiHeaderNames.TrustedClientId, _configuration.ClientId);
            }

            if (Request.AuthorizationRequirement == AuthorizationRequirement.Required)
            {
                if (!_configuration.Authorization.IsAuthorized)
                    throw new ArgumentException(message: "Authorization is invalid and it's required for this request.");

                request.Headers.Authorization = new AuthenticationHeaderValue(ApiHeaderNames.AuthorizationHeader, _configuration.Authorization.Token);
            }

            if (Request.AuthorizationRequirement == AuthorizationRequirement.Optimal
                && _configuration.Authorization.IsAuthorized)
                request.Headers.Authorization = new AuthenticationHeaderValue(ApiHeaderNames.AuthorizationHeader, _configuration.Authorization.Token);
        }
        
        (HttpContent httpContent, string jsonContent) GetRequestBodyContent()
        {
            var jsonBody = string.Empty;

            if (_requestBody == null)
                return (null, jsonBody);

            jsonBody = JsonHelpers.Serialize(_requestBody);

            if (!string.IsNullOrEmpty(jsonBody))
                return (new StringContent(jsonBody, Encoding.UTF8, ContentTypeNames.Json), jsonBody);

            return (null, string.Empty);
        }

        /// <summary> Builds the URL for this request. </summary>
        /// <returns> <see cref="Uri" /> instance of request. </returns>
        Uri BuildUrl()
        {
            var uriTemplate = new UriTemplate(Request.UriTemplateParameters);
            var pathParameters = Request.GetUrlParameters();

            foreach (var pair in pathParameters)
                uriTemplate.AddParameter(pair.Key, pair.Value);

            var uri = uriTemplate.Resolve();
            return new Uri(_configuration.BaseUrl, uri);
        }
    }
}