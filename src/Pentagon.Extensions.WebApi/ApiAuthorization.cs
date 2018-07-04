// -----------------------------------------------------------------------
//  <copyright file="ApiAuthorization.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.WebApi
{
    public class ApiAuthorization
    {
        /// <inheritdoc />
        public bool IsAuthorized => Token != null;

        /// <inheritdoc />
        public string Token { get; set; }
    }
}