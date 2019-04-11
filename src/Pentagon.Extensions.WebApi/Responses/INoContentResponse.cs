// -----------------------------------------------------------------------
//  <copyright file="INoContentResponse.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.WebApi.Responses
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    
    public interface IBasicResponse
    {
        bool IsSuccessful { get; set; }

        ApiException Exception { get; set; }

        HttpStatusCode StatusCode { get; set; }

        string ReasonPhrase { get; set; }

        string Content { get; set; }
    }

    public interface IHeadResponse<THeaders> : IBasicResponse
        where THeaders : IApiResponseHeaders
    {
        THeaders Headers { get; set; }
    }

    public interface IContentResponse<out TContent> : IBasicResponse
    {
        bool HasValue { get; }

        TContent Value { get; }
    }
}