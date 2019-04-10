// -----------------------------------------------------------------------
//  <copyright file="IRequestMessage.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.WebApi.Interfaces
{
    using System;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using Requests;

    public interface IRequestMessage
    {
        IRequest Request { get; }

        Uri RequestUri { get; }

        HttpRequestHeaders Headers { get; }

        string Url { get; set; }

        string RequestBodyJson { get; set; }
    }
}