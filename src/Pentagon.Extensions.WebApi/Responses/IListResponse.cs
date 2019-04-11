// -----------------------------------------------------------------------
//  <copyright file="IListResponse.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.WebApi.Responses
{
    using System.Collections.Generic;

    public interface IListResponse<out TContent, THeaders> : IResponse<IEnumerable<TContent>, THeaders>, IEnumerable<TContent>
            where THeaders : IApiResponseHeaders
    { }
}