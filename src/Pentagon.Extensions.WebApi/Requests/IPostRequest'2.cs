// -----------------------------------------------------------------------
//  <copyright file="IPostRequest'2.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.WebApi.Requests
{
    using Interfaces;

    public interface IPostRequest<TContent, TRequestBody> : IRequest<TContent>, IHasRequestBody<TRequestBody> { }
}