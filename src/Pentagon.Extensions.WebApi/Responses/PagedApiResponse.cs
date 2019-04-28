// -----------------------------------------------------------------------
//  <copyright file="PagedApiResponse.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.WebApi.Responses
{
    public class PagedApiResponse<TContent> : ListApiResponse<TContent>, IPagedResponse<TContent>
    {
    }
}