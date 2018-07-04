// -----------------------------------------------------------------------
//  <copyright file="IPostRequest.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.WebApi.Requests
{
    using Interfaces;

    public interface IPostRequest<TRequestBody> : IRequest, IHasRequestBody<TRequestBody> { }
}