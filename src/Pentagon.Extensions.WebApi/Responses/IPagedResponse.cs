// -----------------------------------------------------------------------
//  <copyright file="IPagedResponse.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.WebApi.Responses
{
    public interface IPagedResponse<out TContent, THeaders> : IListResponse<TContent, THeaders>
            where THeaders : IApiResponseHeaders { }
}