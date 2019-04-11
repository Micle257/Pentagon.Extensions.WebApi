// -----------------------------------------------------------------------
//  <copyright file="IResponse.cs">
//   Copyright (c) Michal Pokorn�. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.WebApi.Responses
{
    public interface IResponse<out TContent, THeaders> : IHeadResponse<THeaders>, IContentResponse<TContent>
            where THeaders : IApiResponseHeaders { }
}