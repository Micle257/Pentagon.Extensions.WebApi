// -----------------------------------------------------------------------
//  <copyright file="ApiConfiguration.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.WebApi
{
    using System;
    using Abstractions;
    using Microsoft.Extensions.Options;

    public class ApiConfiguration<T> : IApiConfiguration, IOptionsSnapshot<T>
            where T : ApiOptions, new()
    {
        readonly IOptionsSnapshot<T> _snapshot;

        public ApiConfiguration(IOptionsSnapshot<T> snapshot)
        {
            _snapshot = snapshot;
        }

        /// <inheritdoc />
        public int ApiVersion => Value.ApiVersion;

        /// <inheritdoc />
        public Uri BaseUrl => Value.BaseUrl;

        /// <inheritdoc />
        public string ClientId => Value.ClientId;

        /// <inheritdoc />
        public ApiAuthorization Authorization { get; } = new ApiAuthorization();

        /// <inheritdoc />
        public T Value => _snapshot.Value;

        /// <inheritdoc />
        public T Get(string name) => _snapshot.Get(name);
    }
}