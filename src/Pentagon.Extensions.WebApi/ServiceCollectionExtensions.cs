// -----------------------------------------------------------------------
//  <copyright file="ServiceCollectionExtensions.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.WebApi
{
    using Abstractions;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApiConfiguration<TOptions>(this IServiceCollection services)
                where TOptions : ApiOptions, new()
        {
            services.AddScoped<IApiConfiguration>(p => new ApiConfiguration<TOptions>(p.GetRequiredService<IOptionsSnapshot<TOptions>>()));

            return services;
        }

        public static IServiceCollection AddDefaultApiConfiguration(this IServiceCollection services) => services.AddApiConfiguration<ApiOptions>();

        public static IServiceCollection AddWebApiCore(this IServiceCollection services)
        {
            services.AddScoped<IRequestHandlerFactory, RequestHandlerFactory>();
            services.AddScoped<IRequestHandler, RequestHandler>();
            services.AddScoped<IRequestMessageBuilder, RequestMessageBuilder>();

            return services;
        }
    }
}