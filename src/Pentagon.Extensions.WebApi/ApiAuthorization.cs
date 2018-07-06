// -----------------------------------------------------------------------
//  <copyright file="ApiAuthorization.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.WebApi
{
    /// <summary> Contains information about API authorization such as token. </summary>
    public class ApiAuthorization
    {
        /// <summary> Gets a value indicating whether the token is set. </summary>
        /// <value> <c> true </c> if the token is set; otherwise, <c> false </c>. </value>
        public bool IsAuthorized => Token != null;

        /// <summary> Gets or sets the token. </summary>
        /// <value> The token. </value>
        public string Token { get; set; }
    }
}