// -----------------------------------------------------------------------
//  <copyright file="ISupportsPagination.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.WebApi.Interfaces
{
    public interface ISupportsPagination
    {
        int? Page { get; set; }
        int? Limit { get; set; }
    }
}