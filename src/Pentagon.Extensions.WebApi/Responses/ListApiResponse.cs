// -----------------------------------------------------------------------
//  <copyright file="ListApiResponse.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.WebApi.Responses
{
    using System.Collections;
    using System.Collections.Generic;

    public class ListApiResponse<TContent> : ApiResponse<IEnumerable<TContent>>, IListResponse<TContent>
    {
        /// <inheritdoc />
        public string SortBy { get; set; }

        /// <inheritdoc />
        public string SortHow { get; set; }

        /// <inheritdoc />
        public IEnumerator<TContent> GetEnumerator() => Value.GetEnumerator();

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}