// -----------------------------------------------------------------------
//  <copyright file="RequestMessage.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.WebApi
{
    using System;
    using System.Net.Http;
    using Interfaces;
    using Requests;

    /// <summary> Represents a HTTP request message defined in TraktAPI. </summary>
    public class RequestMessage : HttpRequestMessage, IRequestMessage
    {
        /// <summary> Initializes a new instance of the <see cref="RequestMessage" /> class. </summary>
        /// <param name="method"> The HTTP method. </param>
        /// <param name="requestUri"> The <see cref="T:System.Uri" /> to request. </param>
        public RequestMessage(HttpMethod method, Uri requestUri) : base(method, requestUri) { }

        protected RequestMessage(RequestMessage request) : this(request.Method, request.RequestUri)
        {
            Content = request.Content;
            Version = request.Version;
            Request = request.Request;
            Url = request.Url;
            RequestBodyJson = request.RequestBodyJson;
        }

        /// <inheritdoc />
        public IRequest Request { get; set; }

        public string Url { get; set; }

        public string RequestBodyJson { get; set; }
    }
}