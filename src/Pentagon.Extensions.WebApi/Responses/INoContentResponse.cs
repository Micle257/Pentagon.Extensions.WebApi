// -----------------------------------------------------------------------
//  <copyright file="INoContentResponse.cs">
//   Copyright (c) Michal Pokorn�. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.WebApi.Responses
{
    using System;
    using System.Net;

    public interface INoContentResponse
    {
        bool IsSuccess { get; }

        Exception Exception { get; }
        
        HttpStatusCode StatusCode { get; }

        string ReasonPhrase { get; }
    }
}