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

    public interface IResponse
    {
        bool IsSuccess { get; }

        Exception Exception { get; }

        HttpStatusCode StatusCode { get; }

        string ReasonPhrase { get; }

        HttpResponseHeaders Headers { get; }
    }
}