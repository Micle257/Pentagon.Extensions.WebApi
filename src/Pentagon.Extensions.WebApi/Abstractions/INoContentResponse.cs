// -----------------------------------------------------------------------
//  <copyright file="INoContentResponse.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.WebApi.Abstractions
{
    using System;

    public interface INoContentResponse
    {
        bool IsSuccess { get; }

        Exception Exception { get; }
    }
}