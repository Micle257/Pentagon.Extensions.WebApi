// -----------------------------------------------------------------------
//  <copyright file="ApiOptions.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.WebApi
{
    using System;

    public class ApiOptions
    {
        public Uri BaseUrl => Url == null ? null : new Uri(Url);
        public int ApiVersion { get; set; } = 1;
        public string Url { get; set; }
        public string ClientId { get; set; }
    }
}