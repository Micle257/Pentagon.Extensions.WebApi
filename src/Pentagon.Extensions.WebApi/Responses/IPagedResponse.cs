// -----------------------------------------------------------------------
//  <copyright file="IPagedResponse.cs">
//   Copyright (c) Michal Pokorn�. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.WebApi.Responses
{
    using Interfaces;

    public interface IPagedResponse<out TContent> : IListResponse<TContent>, IPagedResponseHeaders { }
}