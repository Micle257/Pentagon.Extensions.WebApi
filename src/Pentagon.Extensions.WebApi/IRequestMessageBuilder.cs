namespace Pentagon.Extensions.WebApi {
    using System;
    using Interfaces;
    using Requests;

    public interface IRequestMessageBuilder
    {
        IRequestMessageBuilder AddBaseUrl(Uri uri);

        IRequestMessageBuilder AddRequest(IRequest request);

        IRequestMessage Build();
    }
}