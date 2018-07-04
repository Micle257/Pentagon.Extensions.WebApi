// -----------------------------------------------------------------------
//  <copyright file="IHasRequestBody.cs">
//   Copyright (c) Michal Pokorn�. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.WebApi.Interfaces
{
    public interface IHasRequestBody<TRequestBody>
    {
        TRequestBody RequestBody { get; set; }
    }
}