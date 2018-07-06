// -----------------------------------------------------------------------
//  <copyright file="RequestHandlerFactory.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.WebApi
{
    using Abstractions;
    using JetBrains.Annotations;

    public class RequestHandlerFactory : IRequestHandlerFactory
    {
        [NotNull]
        readonly IApiConfiguration _configuration;

        public RequestHandlerFactory([NotNull] IApiConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IRequestHandler Create() => new RequestHandler(_configuration);
    }
}