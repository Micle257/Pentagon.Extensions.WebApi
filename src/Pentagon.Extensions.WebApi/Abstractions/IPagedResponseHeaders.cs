// -----------------------------------------------------------------------
//  <copyright file="IPagedResponseHeaders.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.WebApi.Abstractions
{
    public interface IPagedResponseHeaders
    {
        int? Page { get; set; }

        int? Limit { get; set; }

        int? PageCount { get; set; }

        int? ItemCount { get; set; }
    }
}