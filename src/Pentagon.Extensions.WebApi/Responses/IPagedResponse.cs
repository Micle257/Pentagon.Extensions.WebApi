// -----------------------------------------------------------------------
//  <copyright file="IPagedResponse.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.WebApi.Responses
{
    using Interfaces;

    public interface IPagedResponse<TContent> : IListResponse<TContent>, IPagedResponseHeaders { }
}