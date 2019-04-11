// -----------------------------------------------------------------------
//  <copyright file="NoContentResponse.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.WebApi.Responses
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Net;
    using System.Net.Http.Headers;
    using Interfaces;

    public class NoContentResponse<THeaders> : IHeadResponse<THeaders>
            where THeaders : IApiResponseHeaders
    {
        public NoContentResponse()
        {
            
        }

        internal NoContentResponse(IHeadResponse<THeaders> toCopy)
        {
            IsSuccessful = toCopy.IsSuccessful;
            Exception = toCopy.Exception;
            StatusCode = toCopy.StatusCode;
            ReasonPhrase = toCopy.ReasonPhrase;

            if (toCopy.Headers is THeaders head)
            {
                Headers = head;
            }
            else
            {
                var h = Activator.CreateInstance<THeaders>();

                h.BaseHeaders = toCopy.Headers.BaseHeaders;

                Headers = h;
            }

            Content = toCopy.Content;
        }

        public bool IsSuccessful { get; set; }

        /// <inheritdoc />
        public ApiException Exception { get; set; }

        /// <inheritdoc />
        public HttpStatusCode StatusCode { get; set; }

        /// <inheritdoc />
        public string ReasonPhrase { get; set; }

        /// <inheritdoc />
        public THeaders Headers { get; set; }

        /// <inheritdoc />
        public string Content { get; set; }
    }
}