// -----------------------------------------------------------------------
//  <copyright file="IHasRequestBody.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.WebApi.Interfaces
{
    public interface IHasRequestBody
    {
        object RequestBody { get; set; }
    }

    public interface IHasRequestBody<T> : IHasRequestBody
    {
        new T RequestBody { get; set; }
    }
}