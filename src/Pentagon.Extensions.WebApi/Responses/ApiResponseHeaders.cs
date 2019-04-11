// -----------------------------------------------------------------------
//  <copyright file="ApiResponseHeaders.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.WebApi.Responses
{
    using System.Collections.Generic;
    using System.Net.Http.Headers;

    public class ApiResponseHeaders : IApiResponseHeaders
    {
        public HttpResponseHeaders BaseHeaders { get; set; }

        /// <inheritdoc />
        public virtual IDictionary<string, object> GetApiHeaders() => new Dictionary<string, object>();
    }
}