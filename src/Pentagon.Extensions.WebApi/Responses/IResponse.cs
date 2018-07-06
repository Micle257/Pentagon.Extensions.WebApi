// -----------------------------------------------------------------------
//  <copyright file="IResponse.cs">
//   Copyright (c) Michal Pokorn�. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.WebApi.Responses
{
    public interface IResponse<TContent> : INoContentResponse
    {
        bool HasValue { get; }
        TContent Value { get; }
        string RawContent { get; }
    }
}