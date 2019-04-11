// -----------------------------------------------------------------------
//  <copyright file="ApiExceptionArguments.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.WebApi
{
    using System.Net;
    using Interfaces;
    using Responses;

    public class ApiExceptionArguments
    {
        /// <inheritdoc />
        public ApiExceptionArguments(IRequestMessage requestMessage, IBasicResponse responseMessage)
        {
            RequestMessage = requestMessage;
            ResponseMessage = responseMessage;
        }

        public ApiExceptionArguments() { }
        public IRequestMessage RequestMessage { get; }

        public IBasicResponse ResponseMessage { get; }

        /// <summary> Returns the response's status code. </summary>
        public HttpStatusCode? StatusCode => ResponseMessage?.StatusCode;

        /// <summary> Gets or sets the request url. </summary>
        public string RequestUrl => RequestMessage?.Url;

        /// <summary> Gets or sets the request body. </summary>
        public string RequestBody => RequestMessage?.RequestBodyJson;

        /// <summary> Gets or sets the server reason phrase. </summary>
        public string ServerReasonPhrase => ResponseMessage?.ReasonPhrase;
    }
}