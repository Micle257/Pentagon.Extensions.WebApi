// -----------------------------------------------------------------------
//  <copyright file="IHasRequestAuthorization.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.WebApi.Abstractions
{
    /// <summary> Represents a request that has authorization information. </summary>
    public interface IHasRequestAuthorization
    {
        /// <summary> Gets the authorization requirement. </summary>
        /// <value> The authorization requirement. </value>
        AuthorizationRequirement AuthorizationRequirement { get; }
    }
}