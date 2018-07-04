// -----------------------------------------------------------------------
//  <copyright file="IWebApiAuthentication.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.WebApi.Abstractions
{
    using System.Threading.Tasks;
    using Interfaces;

    public interface IWebApiAuthentication
    {
        Task<bool> AuthenticateUserAsync(IAuthenticateApiCredentials credentials);

        Task<bool> RepudiateUserAsync();
    }
}