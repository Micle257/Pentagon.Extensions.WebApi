// -----------------------------------------------------------------------
//  <copyright file="IContentResponse.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.WebApi.Responses
{
    public interface IContentResponse<TContent> : IBasicResponse
    {
        bool HasValue { get; set; }

        TContent Value { get; set; }
    }
}