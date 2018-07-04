// -----------------------------------------------------------------------
//  <copyright file="WebApiAuthentication.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.WebApi
{
    using System.Threading.Tasks;
    using Abstractions;
    using Interfaces;

    public abstract class WebApiAuthentication<TCredentials> : IWebApiAuthentication
            where TCredentials : class, IAuthenticateApiCredentials
    {
        /// <inheritdoc />
        public Task<bool> AuthenticateUserAsync(IAuthenticateApiCredentials credentials) => AuthenticateUserCoreAsync(credentials as TCredentials);

        /// <inheritdoc />
        public abstract Task<bool> RepudiateUserAsync();

        protected abstract Task<bool> AuthenticateUserCoreAsync(TCredentials model);
    }
}