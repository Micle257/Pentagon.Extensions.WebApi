// -----------------------------------------------------------------------
//  <copyright file="IHasRequestBody.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.WebApi.Interfaces
{
    public interface IRequestWithBody
    {
        object RequestBody { get; set; }
    }

    public interface IRequestWithBody<T> : IRequestWithBody
    {
        new T RequestBody { get; set; }
    }
}