// -----------------------------------------------------------------------
//  <copyright file="IPagedResponse.cs">
//   Copyright (c) Michal Pokorn�. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.WebApi.Abstractions
{
    public interface IPagedResponse<TContent> : IListResponse<TContent>, IPagedResponseHeaders { }
}