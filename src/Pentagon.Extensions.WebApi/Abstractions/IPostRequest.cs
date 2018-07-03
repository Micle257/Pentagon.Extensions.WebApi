// -----------------------------------------------------------------------
//  <copyright file="IPostRequest.cs">
//   Copyright (c) Michal Pokorn�. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.WebApi.Abstractions
{
    public interface IPostRequest<TRequestBody> : IRequest, IHasRequestBody<TRequestBody> { }
}