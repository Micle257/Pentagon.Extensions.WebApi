// -----------------------------------------------------------------------
//  <copyright file="ApiExceptionArguments.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.WebApi
{
    using System.Net;

    public class ApiExceptionArguments
    {
        /// <inheritdoc />
        public ApiExceptionArguments(string requestUrl, string requestBody, string response, string serverReasonPhrase, HttpStatusCode statusCode = default)
        {
            StatusCode = statusCode;
            RequestUrl = requestUrl;
            RequestBody = requestBody;
            Response = response;
            ServerReasonPhrase = serverReasonPhrase;
        }

        public ApiExceptionArguments() { }

        /// <summary> Returns the response's status code. </summary>
        public HttpStatusCode StatusCode { get; set; }

        /// <summary> Gets or sets the request url. </summary>
        public string RequestUrl { get; set; }

        /// <summary> Gets or sets the request body. </summary>
        public string RequestBody { get; set; }

        /// <summary> Gets or sets the response content. </summary>
        public string Response { get; set; }

        /// <summary> Gets or sets the server reason phrase. </summary>
        public string ServerReasonPhrase { get; set; }
    }
}