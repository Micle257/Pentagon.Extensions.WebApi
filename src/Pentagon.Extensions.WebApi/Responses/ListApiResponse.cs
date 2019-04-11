// -----------------------------------------------------------------------
//  <copyright file="ListApiResponse.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.WebApi.Responses
{
    using System.Collections;
    using System.Collections.Generic;

    public class ListApiResponse<TContent, THeaders> : ApiResponse<IEnumerable<TContent>, THeaders>, IListResponse<TContent, THeaders>
            where THeaders : IApiResponseHeaders
    {
        public ListApiResponse()
        {
            
        }

        internal ListApiResponse(IHeadResponse<THeaders> response) : base(response)
        {
        }

        /// <inheritdoc />
        public IEnumerator<TContent> GetEnumerator() => Value.GetEnumerator();

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}