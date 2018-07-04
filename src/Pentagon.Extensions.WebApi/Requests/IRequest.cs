// -----------------------------------------------------------------------
//  <copyright file="IRequest.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.WebApi.Requests
{
    using System.Collections.Generic;
    using Interfaces;

    public interface IRequest : IHttpRequest, IHasRequestAuthorization
    {
        string UriTemplate { get; }
        string UriTemplateParameters { get; }
        IDictionary<string, object> GetUriPathParameters();
        void Validate();
    }
}