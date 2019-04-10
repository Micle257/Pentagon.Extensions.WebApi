// -----------------------------------------------------------------------
//  <copyright file="IApiConfiguration.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.WebApi.Abstractions
{
    using System;

    public interface IApiConfiguration
    {
        int ApiVersion { get; }

        Uri BaseUrl { get; }
    }
}