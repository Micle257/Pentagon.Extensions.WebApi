// -----------------------------------------------------------------------
//  <copyright file="IRequest.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.WebApi.Requests
{
    using System;
    using System.Collections.Generic;
    using Interfaces;

    public interface IRequest : IHttpRequest
    {
        string UriTemplate { get; }

        string UriTemplateParameters { get; }
        
        IDictionary<string, object> GetUrlParameters();

        RequestValidationResult Validate();
    }

    public interface IRequest<T> : IRequest
    {
    }
}