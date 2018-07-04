// -----------------------------------------------------------------------
//  <copyright file="ISortingResponseHeaders.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.WebApi.Interfaces
{
    /// <summary> Represents the response headers. </summary>
    public interface ISortingResponseHeaders
    {
        string SortBy { get; set; }

        string SortHow { get; set; }
    }
}