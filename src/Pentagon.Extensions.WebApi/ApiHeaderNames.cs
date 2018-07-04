// -----------------------------------------------------------------------
//  <copyright file="ApiHeaderNames.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.WebApi
{
    public static class ApiHeaderNames
    {
        public const string AuthorizationHeader = "Bearer";

        public const string TrustedClientId = "X-Trusted-Client-Id";

        public static class PaginationHeaders
        {
            public const string Page = "X-Pagination-Page";
            public const string Limit = "X-Pagination-Limit";
            public const string PageCount = "X-Pagination-Page-Count";
            public const string ItemCount = "X-Pagination-Item-Count";
        }

        public static class SortHeaders
        {
            public const string SortBy = "X-Sort-By";
            public const string SortHow = "X-Sort-How";
        }
    }
}