// -----------------------------------------------------------------------
//  <copyright file="RequestMessageBuilder.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.WebApi
{
    using System;
    using System.Net.Http;
    using System.Text;
    using Abstractions;
    using Tavis.UriTemplates;
    using Utilities.Data.Json;

    public abstract class RequestMessageBuilder : IRequestMessageBuilder
    {
        readonly IApiConfiguration _configuration;

        object _requestBody;

        public RequestMessageBuilder(IApiConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IRequestMessage RequestMessage { get; protected set; }

        protected IRequest Request { get; private set; }

        public IRequestMessageBuilder WithRequest(IRequest request)
        {
            Request = request;
            return this;
        }

        public IRequestMessageBuilder WithPostRequest<TRequestBody>(IHasRequestBody<TRequestBody> request)
        {
            Request = request as IRequest;
            _requestBody = request.RequestBody;
            return this;
        }

        public IRequestMessageBuilder BuildRequestMessage()
        {
            CreateRequestMessage();
            SetRequestMessageHeadersForAuthorization();

            if (_requestBody != null)
                AddBodyContent();

            return this;
        }

        protected virtual IRequestMessage CreateRequestMessage()
        {
            var url = BuildUrl();
            RequestMessage = new RequestMessage(Request.Method, url) {Url = url.AbsoluteUri};

            return RequestMessage;
        }

        /// <summary> Sets the request message headers for authorization by given <see cref="AuthorizationRequirement" />. </summary>
        protected abstract void SetRequestMessageHeadersForAuthorization();

        void AddBodyContent()
        {
            var content = GetRequestBodyContent();
            RequestMessage.Content = content.httpContent;
            RequestMessage.RequestBodyJson = content.jsonContent;
        }

        (HttpContent httpContent, string jsonContent) GetRequestBodyContent()
        {
            var jsonBody = string.Empty;

            if (_requestBody == null)
                return (null, jsonBody);

            jsonBody = JsonHelpers.Serialize(_requestBody);

            if (!string.IsNullOrEmpty(jsonBody))
                return (new StringContent(jsonBody, Encoding.UTF8, ApiNames.ContentTypes.Json), jsonBody);

            return (null, string.Empty);
        }

        /// <summary> Builds the URL for this request. </summary>
        /// <returns> <see cref="Uri" /> instance of request. </returns>
        Uri BuildUrl()
        {
            var uriTemplate = new UriTemplate(Request.UriTemplateParameters);
            var pathParameters = Request.GetUriPathParameters();

            foreach (var pair in pathParameters)
                uriTemplate.AddParameter(pair.Key, pair.Value);

            var uri = uriTemplate.Resolve();
            return new Uri(_configuration.BaseUrl, uri);
        }
    }
}