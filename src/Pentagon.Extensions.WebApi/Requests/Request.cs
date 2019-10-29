// -----------------------------------------------------------------------
//  <copyright file="Request.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.WebApi.Requests
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Reflection;
    using Attributes;

    public abstract class Request<T> : IRequest<T>
    {
        /// <inheritdoc />
        public TimeSpan Timeout { get; set; } = TimeSpan.FromMinutes(1);

        /// <inheritdoc />
        public abstract HttpMethod Method { get; }

        /// <inheritdoc />
        public abstract string UriTemplate { get; }

        /// <inheritdoc />
        public RequestValidationResult Validate()
        {
            var builder = new RequestValidationBuilder();

            ValidateCore(builder);

            return builder.Build();
        }

        /// <inheritdoc />
        public IDictionary<string, object> GetUrlQueryParameters()
        {
            var result = new Dictionary<string, object>();

            var properties = GetType().GetProperties();

            foreach (var property in properties)
            {
                var attribute = property.GetCustomAttribute<UrlQueryParameterAttribute>(true);

                var value = property.GetValue(this);

                if (!string.IsNullOrWhiteSpace(attribute?.Name))
                    result.Add(attribute.Name, value);
            }

            return result;
        }

        /// <inheritdoc />
        public IDictionary<string, object> GetUrlPathParameters()
        {
            var result = new Dictionary<string, object>();

            var properties = GetType().GetProperties();

            foreach (var property in properties)
            {
                var attribute = property.GetCustomAttribute<UrlPathParameterAttribute>(true);

                var value = property.GetValue(this);

                if (!string.IsNullOrWhiteSpace(attribute?.Name))
                    result.Add(attribute.Name, value.ToString());
            }

            return result;
        }

        protected virtual void ValidateCore(RequestValidationBuilder builder) { }
    }
}