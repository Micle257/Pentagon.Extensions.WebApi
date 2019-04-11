// -----------------------------------------------------------------------
//  <copyright file="IBasicResponse.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.WebApi.Responses
{
    using System.Net;

    public interface IBasicResponse
    {
        bool IsSuccessful { get; set; }

        ApiException Exception { get; set; }

        HttpStatusCode StatusCode { get; set; }

        string ReasonPhrase { get; set; }

        string Content { get; set; }
    }
}