// -----------------------------------------------------------------------
//  <copyright file="Request.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.WebApi.Requests
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Web;

    public abstract class Request<T> : IRequest<T>
    {
        /// <inheritdoc />
        public abstract HttpMethod Method { get; }

        /// <inheritdoc />
        public abstract string UriTemplate { get; }

        public string UriTemplateParameters
        {
            get
            {
                var uriPath = GetUrlQueryParameters();

                if (uriPath?.Any() == false)
                    return UriTemplate;

                var parms = uriPath.Keys.Aggregate((a, b) => $"{a},{b}");

                var url = HttpUtility.HtmlEncode(parms).Replace(oldValue: "-", newValue: "%2D");

                if (!string.IsNullOrEmpty(url))
                    return UriTemplate + $"{{?{url}}}";

                return UriTemplate;
            }
        }

        /// <inheritdoc />
        public TimeSpan Timeout { get; set; } = TimeSpan.FromMinutes(1);

        /// <inheritdoc />
        public RequestValidationResult Validate()
        {
            var builder = new RequestValidationBuilder();

            ValidateCore(builder);

            return builder.Build();
        }

        /// <inheritdoc />
        public IDictionary<string, object> GetUrlParameters() => GetUrlPathParameters().Concat(GetUrlQueryParameters()).ToDictionary(a => a.Key, a => a.Value);

        /// <inheritdoc />
        protected virtual IDictionary<string, object> GetUrlQueryParameters() => new ConcurrentDictionary<string, object>();

        /// <inheritdoc />
        protected virtual IDictionary<string, object> GetUrlPathParameters() => new ConcurrentDictionary<string, object>();

        protected virtual void ValidateCore(RequestValidationBuilder builder) { }
    }
}