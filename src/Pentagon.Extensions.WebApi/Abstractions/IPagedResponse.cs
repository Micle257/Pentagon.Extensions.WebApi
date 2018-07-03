// -----------------------------------------------------------------------
//  <copyright file="IPagedResponse.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.WebApi.Abstractions
{
    public interface IPagedResponse<TContent> : IListResponse<TContent>, IPagedResponseHeaders { }
}