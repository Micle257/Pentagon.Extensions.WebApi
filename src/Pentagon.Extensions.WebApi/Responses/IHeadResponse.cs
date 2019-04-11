// -----------------------------------------------------------------------
//  <copyright file="IHeadResponse.cs">
//   Copyright (c) Michal Pokorn�. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.WebApi.Responses
{
    public interface IHeadResponse<THeaders> : IBasicResponse
            where THeaders : IApiResponseHeaders
    {
        THeaders Headers { get; set; }
    }
}