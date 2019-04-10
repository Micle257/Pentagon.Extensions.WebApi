// -----------------------------------------------------------------------
//  <copyright file="ApiExceptionArguments.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.WebApi
{
    using Responses;

    public class ApiExceptionArguments
    {
        /// <inheritdoc />
        public ApiExceptionArguments(string requestUrl, string requestBody, IResponse response)
        {
            RequestUrl = requestUrl;
            RequestBody = requestBody;
            Response = response;
        }

        public ApiExceptionArguments() { }

        /// <summary> Gets or sets the request url. </summary>
        public string RequestUrl { get; set; }

        /// <summary> Gets or sets the request body. </summary>
        public string RequestBody { get; set; }

        /// <summary> Gets or sets the response content. </summary>
        public IResponse Response { get; set; }
    }
}