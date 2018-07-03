// -----------------------------------------------------------------------
//  <copyright file="IPostRequest'2.cs">
//   Copyright (c) Michal Pokorn�. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.WebApi.Abstractions
{
    public interface IPostRequest<TContent, TRequestBody> : IRequest<TContent>, IHasRequestBody<TRequestBody> { }
}