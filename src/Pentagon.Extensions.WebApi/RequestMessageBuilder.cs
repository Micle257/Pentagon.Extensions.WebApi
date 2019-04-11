// -----------------------------------------------------------------------
//  <copyright file="RequestMessageBuilder.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.WebApi
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Text;
    using Interfaces;
    using IO.Json;
    using Requests;
    using Tavis.UriTemplates;

    public class RequestMessageBuilder : IRequestMessageBuilder
    {
        object _requestBody;

        protected IRequest Request { get; private set; }
        
        protected Uri BaseUrl { get; private set; }

        public IRequestMessageBuilder AddBaseUrl(Uri uri)
        {
            BaseUrl = uri;
            return this;
        }

        public IRequestMessageBuilder AddRequest(IRequest request)
        {
            Request = request;

            if (request is IRequestWithBody body)
                _requestBody = body.RequestBody;

            return this;
        }

        protected virtual IDictionary<string,object> GetUrlQueryParameters()
        {
            var requestQueryParameters = Request.GetUrlQueryParameters();

            return requestQueryParameters;
        }

        protected virtual string GetUrlTemplate() => Request.UriTemplate;

        public IRequestMessage Build()
        {
            var url = BuildUrl();

            var request = new RequestMessage(Request.Method, url)
                          {
                                  Url = url.AbsoluteUri,
                                  Request = Request
                          };

            if (_requestBody != null)
            {
                var content = GetRequestBodyContent();
                request.Content = content.httpContent;
                request.RequestBodyJson = content.jsonContent;
            }

            request = BuildCore(request);

            return request;
        }

        protected virtual RequestMessage BuildCore(RequestMessage request) => request;

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
            var urlTemplateText = GetUrlTemplate();

            var uriTemplate = new UriTemplate(urlTemplateText);

            var pathParameters = Request.GetUrlPathParameters();

            foreach (var pair in pathParameters)
                uriTemplate.AddParameter(pair.Key, pair.Value);

            var queryParameters = GetUrlQueryParameters();

            foreach (var pair in queryParameters)
                uriTemplate.AddParameter(pair.Key, pair.Value);

            var uri = uriTemplate.Resolve();
            return new Uri(BaseUrl, uri);
        }
    }
}