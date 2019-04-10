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
    using Interfaces;
    using IO.Json;
    using Requests;
    using Tavis.UriTemplates;

    public interface IRequestMessageBuilder
    {
        IRequestMessageBuilder AddBaseUrl(Uri uri);

        IRequestMessageBuilder AddRequest(IRequest request);

        IRequestMessage Build();
    }

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

            if (request is IHasRequestBody body)
                _requestBody = body.RequestBody;

            return this;
        }
        
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
            var uriTemplate = new UriTemplate(Request.UriTemplateParameters);
            var pathParameters = Request.GetUrlParameters();

            foreach (var pair in pathParameters)
                uriTemplate.AddParameter(pair.Key, pair.Value);

            var uri = uriTemplate.Resolve();
            return new Uri(BaseUrl, uri);
        }
    }
}