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
        public ListApiResponse()
        {
            
        }

        internal ListApiResponse(IResponse response) : base(response)
        {
        }

        /// <inheritdoc />
        public IEnumerator<TContent> GetEnumerator() => Value.GetEnumerator();

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}