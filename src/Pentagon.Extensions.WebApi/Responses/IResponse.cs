// -----------------------------------------------------------------------
//  <copyright file="IResponse.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.WebApi.Responses
{
    using System.Net.Http.Headers;

    public interface IResponse<out TContent, THeaders> : IHeadResponse<THeaders>, IContentResponse<TContent>
            where THeaders : IApiResponseHeaders
    {
    }
}