// -----------------------------------------------------------------------
//  <copyright file="NoContentResponse.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.WebApi.Responses
{
    using System;
    using System.Net;

    public class NoContentResponse : IBasicResponse
    {
        public bool IsSuccessful { get; set; }

        /// <inheritdoc />
        public ApiException Exception { get; set; }

        /// <inheritdoc />
        public HttpStatusCode StatusCode { get; set; }

        /// <inheritdoc />
        public string ReasonPhrase { get; set; }

        /// <inheritdoc />
        public string Content { get; set; }

        /// <inheritdoc />
        public IApiResponseHeaders Headers { get; set; }
    }
}