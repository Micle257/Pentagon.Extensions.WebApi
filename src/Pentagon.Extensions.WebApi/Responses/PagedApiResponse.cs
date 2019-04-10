// -----------------------------------------------------------------------
//  <copyright file="PagedApiResponse.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.WebApi.Responses
{
    using System.Net.Http.Headers;
    using Interfaces;

    public class PagedApiResponse<TContent> : ListApiResponse<TContent>, IPagedResponse<TContent>
    {
        public PagedApiResponse()
        {

        }

        internal PagedApiResponse(IResponse response) : base(response)
        {
            if (response is IPagedResponseHeaders pagedResponseHeaders)
            {
                Page = pagedResponseHeaders.Page;
                Limit = pagedResponseHeaders.Limit;
                PageCount = pagedResponseHeaders.PageCount;
                ItemCount = pagedResponseHeaders.ItemCount;
            }
        }

        public int? Page { get; set; }

        public int? Limit { get; set; }

        public int? PageCount { get; set; }

        public int? ItemCount { get; set; }
    }
}