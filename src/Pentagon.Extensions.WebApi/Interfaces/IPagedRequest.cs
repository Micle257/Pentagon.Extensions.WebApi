// -----------------------------------------------------------------------
//  <copyright file="IPagedRequest.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.WebApi.Interfaces
{
    public interface IPagedRequest
    {
        int? Page { get; set; }

        int? Limit { get; set; }
    }
}