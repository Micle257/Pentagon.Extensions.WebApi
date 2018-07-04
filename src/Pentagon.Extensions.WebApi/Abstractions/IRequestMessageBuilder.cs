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
        IRequestMessage RequestMessage { get; }
        IRequestMessageBuilder WithRequest(IRequest request);
        IRequestMessageBuilder WithPostRequest<TRequestBody>(IHasRequestBody<TRequestBody> request);
        IRequestMessageBuilder BuildRequestMessage();
    }
}