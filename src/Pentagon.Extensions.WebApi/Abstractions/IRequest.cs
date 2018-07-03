// -----------------------------------------------------------------------
//  <copyright file="IRequest.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.WebApi.Abstractions
{
    using System.Collections.Generic;

    public interface IRequest : IHttpRequest, IHasRequestAuthorization
    {
        string UriTemplate { get; }
        string UriTemplateParameters { get; }
        IDictionary<string, object> GetUriPathParameters();
        void Validate();
    }
}