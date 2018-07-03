// -----------------------------------------------------------------------
//  <copyright file="IHasRequestBody.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.WebApi.Abstractions
{
    public interface IHasRequestBody<TRequestBody>
    {
        TRequestBody RequestBody { get; set; }
    }
}