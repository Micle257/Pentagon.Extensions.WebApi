// -----------------------------------------------------------------------
//  <copyright file="Request.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.WebApi
{
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Web;
    using Abstractions;

    public abstract class Request<T> : IRequest<T>
    {
        /// <inheritdoc />
        public abstract AuthorizationRequirement AuthorizationRequirement { get; }

        /// <inheritdoc />
        public abstract HttpMethod Method { get; }

        /// <inheritdoc />
        public abstract string UriTemplate { get; }

        public string UriTemplateParameters
        {
            get
            {
                var uriPath = GetUriPathParameters();

                if (!uriPath.Any())
                    return UriTemplate;

                var parms = uriPath.Keys.Aggregate((a, b) => $"{a},{b}");

                var url = HttpUtility.HtmlEncode(parms).Replace(oldValue: "-", newValue: "%2D");

                if (!string.IsNullOrEmpty(url))
                    return UriTemplate + $"{{?{url}}}";

                return UriTemplate;
            }
        }

        /// <inheritdoc />
        public virtual void Validate() { }

        /// <inheritdoc />
        public virtual IDictionary<string, object> GetUriPathParameters() => new ConcurrentDictionary<string, object>();
    }
}