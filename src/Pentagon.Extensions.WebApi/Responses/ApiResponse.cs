// -----------------------------------------------------------------------
//  <copyright file="ApiResponse.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.WebApi.Responses
{
    using System.Net.Http.Headers;

    public class ApiResponse<TContent> : NoContentResponse, IResponse<TContent>
    {
        public bool HasValue { get; set; }

        public TContent Value { get; set; }

        /// <inheritdoc />
        public string RawContent { get; set; }
    }
}