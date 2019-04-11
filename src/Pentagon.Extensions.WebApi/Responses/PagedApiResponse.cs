// -----------------------------------------------------------------------
//  <copyright file="PagedApiResponse.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.WebApi.Responses
{
    public class PagedApiResponse<TContent, THeaders> : ListApiResponse<TContent, THeaders>, IPagedResponse<TContent, THeaders>
            where THeaders : IApiResponseHeaders
    {
        public PagedApiResponse() { }

        internal PagedApiResponse(IHeadResponse<THeaders> response) : base(response) { }
    }
}