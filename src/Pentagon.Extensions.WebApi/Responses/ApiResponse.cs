// -----------------------------------------------------------------------
//  <copyright file="ApiResponse.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.WebApi.Responses
{
    public class ApiResponse<TContent, THeaders> : NoContentResponse<THeaders>, IResponse<TContent, THeaders>
            where THeaders : IApiResponseHeaders
    {
        public ApiResponse() { }

        internal ApiResponse(IHeadResponse<THeaders> response) : base(response)
        {
            if (response is IResponse<TContent, THeaders> contentResponse)
            {
                HasValue = contentResponse.HasValue;
                Value = contentResponse.Value;
            }
            else
            {
                HasValue = false;
                Value = default;
            }
        }

        public bool HasValue { get; set; }

        public TContent Value { get; set; }
    }
}