// -----------------------------------------------------------------------
//  <copyright file="IPagedResponse.cs">
//   Copyright (c) Michal Pokorn�. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.WebApi.Responses
{
    public interface IPagedResponse<out TContent> : IListResponse<TContent> { }
}