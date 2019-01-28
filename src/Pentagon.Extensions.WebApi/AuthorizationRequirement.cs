// -----------------------------------------------------------------------
//  <copyright file="AuthorizationRequirement.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.WebApi
{
    /// <summary> Provides information about authorization required headers; if they are required in a request. </summary>
    public enum AuthorizationRequirement
    {
        Unspecified,

        /// <summary> The authorization header will be indicated. </summary>
        Required,

        /// <summary> The authorization header is not required and won't be in-tented. </summary>
        NotRequired,

        /// <summary> The authorization header is optimal for request. </summary>
        Optimal
    }
}