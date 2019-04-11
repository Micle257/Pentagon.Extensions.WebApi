// -----------------------------------------------------------------------
//  <copyright file="IRequest.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.WebApi.Requests
{
    using System.Collections.Generic;
    using Interfaces;

    public interface IRequest : IHttpRequest
    {
        string UriTemplate { get; }

        IDictionary<string, object> GetUrlPathParameters();

        IDictionary<string, object> GetUrlQueryParameters();

        RequestValidationResult Validate();
    }

    public interface IRequest<T> : IRequest { }
}