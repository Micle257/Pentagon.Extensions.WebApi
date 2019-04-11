// -----------------------------------------------------------------------
//  <copyright file="ApiConfiguration.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.WebApi.Configuration
{
    using System;
    using JetBrains.Annotations;
    using Microsoft.Extensions.Options;

    /// <summary> Represents a wrapper around <see cref="IOptions{TOptions}" />. </summary>
    /// <typeparam name="T"> The type of the options. </typeparam>
    public class ApiConfiguration<T> : IApiConfiguration
            where T : class, IApiConfiguration, new()
    {
        [NotNull]
        protected readonly T Value;

        protected ApiConfiguration(IOptionsFactory<T> snapshot)
        {
            Value = new OptionsManager<T>(snapshot).Value ?? new T();

            ApiVersion = Value.ApiVersion;
            BaseUrl = Value.BaseUrl;
        }

        /// <inheritdoc />
        public int ApiVersion { get; }

        /// <inheritdoc />
        public Uri BaseUrl { get; }
    }
}