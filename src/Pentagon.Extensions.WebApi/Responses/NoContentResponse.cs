// -----------------------------------------------------------------------
//  <copyright file="NoContentResponse.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.WebApi.Responses
{
    using System;
    using System.Net;
    using System.Net.Http.Headers;

    public class NoContentResponse : IResponse
    {
        public bool IsSuccess { get; set; }

        /// <inheritdoc />
        public Exception Exception { get; set; }

        /// <inheritdoc />
        public HttpStatusCode StatusCode { get; set; }

        /// <inheritdoc />
        public string ReasonPhrase { get; set; }

        /// <inheritdoc />
        public HttpResponseHeaders Headers { get; set; }
    }
}