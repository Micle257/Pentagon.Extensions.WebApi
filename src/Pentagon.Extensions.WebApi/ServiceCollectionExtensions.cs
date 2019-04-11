// -----------------------------------------------------------------------
//  <copyright file="ServiceCollectionExtensions.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.WebApi
{
    using Configuration;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApiConfiguration<TOptions>(this IServiceCollection services, IConfiguration configuration = null)
                where TOptions : IApiConfiguration, new()
        {
            services.AddOptions();

            if (configuration != null)
                services.Configure<ApiOptions>(configuration);

            services.AddScoped<IApiConfiguration, ApiConfiguration<ApiOptions>>();

            return services;
        }
    }
}