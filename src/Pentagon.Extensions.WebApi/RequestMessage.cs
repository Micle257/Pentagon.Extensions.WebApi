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

    /// <summary> Represents a HTTP request message defined in TraktAPI. </summary>
    public class RequestMessage : HttpRequestMessage, IRequestMessage
    {
        /// <summary> Initializes a new instance of the <see cref="RequestMessage" /> class. </summary>
        /// <param name="method"> The HTTP method. </param>
        /// <param name="requestUri"> The <see cref="T:System.Uri" /> to request. </param>
        public RequestMessage(HttpMethod method, Uri requestUri) : base(method, requestUri) { }

        public string Url { get; set; }

        public string RequestBodyJson { get; set; }
    }
}