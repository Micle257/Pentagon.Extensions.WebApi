// -----------------------------------------------------------------------
//  <copyright file="IRequestMessageBuilder.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.WebApi.Abstractions
{
    using Interfaces;
    using Requests;

    public interface IRequestMessageBuilder
    {
        IRequestMessageBuilder AddRequest(IRequest request);
        IRequestMessageBuilder AddPostRequest<TRequestBody>(IHasRequestBody<TRequestBody> request);
        IRequestMessage Build();
    }
}