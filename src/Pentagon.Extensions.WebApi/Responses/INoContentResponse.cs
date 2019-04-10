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
        bool IsSuccess { get; set; }

        ApiException Exception { get; set; }

        HttpStatusCode StatusCode { get; set; }

        string ReasonPhrase { get; set; }

        HttpResponseHeaders Headers { get; set; }
        
        string RawContent { get; set; }
    }
}