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
        public ApiResponse()
        {
            
        }

        internal ApiResponse(IResponse response) : base(response)
        {
            if (response is IResponse<TContent> contentResponse)
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