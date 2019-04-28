// -----------------------------------------------------------------------
//  <copyright file="ApiResponse.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.WebApi.Responses
{
    public class ApiResponse<TContent> : NoContentResponse, IResponse<TContent>
    {
        public bool HasValue { get; set; }

        public TContent Value { get; set; }
    }
}