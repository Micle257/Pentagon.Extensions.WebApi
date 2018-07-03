// -----------------------------------------------------------------------
//  <copyright file="IRequestMessageBuilder.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.WebApi
{
    using Abstractions;

    public interface IRequestMessageBuilder
    {
        IRequestMessage RequestMessage { get; }
        IRequestMessageBuilder WithRequest(IRequest request);
        IRequestMessageBuilder WithPostRequest<TRequestBody>(IHasRequestBody<TRequestBody> request);
        IRequestMessageBuilder BuildRequestMessage();
    }
}