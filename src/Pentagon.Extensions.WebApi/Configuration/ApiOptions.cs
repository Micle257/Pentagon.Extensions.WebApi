// -----------------------------------------------------------------------
//  <copyright file="ApiOptions.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.WebApi
{
    using System;
    using Abstractions;

    public class ApiOptions : IApiConfiguration
    {
        public Uri BaseUrl { get; set; }

        public int ApiVersion { get; set; } = 1;
    }
}