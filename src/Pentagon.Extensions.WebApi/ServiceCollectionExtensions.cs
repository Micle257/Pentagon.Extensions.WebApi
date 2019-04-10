// -----------------------------------------------------------------------
//  <copyright file="ServiceCollectionExtensions.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.WebApi
{
    using Abstractions;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApiConfiguration<TOptions>(this IServiceCollection services, IConfiguration configuration = null)
                where TOptions : ApiOptions, new()
        {
            services.AddOptions();

            if (configuration != null)
                services.Configure<ApiOptions>(configuration);

            services.AddScoped<IApiConfiguration, ApiConfiguration<ApiOptions>>();

            return services;
        }

        public static IServiceCollection AddDefaultApiConfiguration(this IServiceCollection services) => services.AddApiConfiguration<ApiOptions>();

        public static IServiceCollection AddWebApiCore(this IServiceCollection services)
        {
            services.AddScoped<IRequestHandler, RequestHandler>();

            return services;
        }
    }
}