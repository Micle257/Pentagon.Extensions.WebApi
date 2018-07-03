﻿// -----------------------------------------------------------------------
//  <copyright file="IRequestMessage.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.WebApi
{
    using System;
    using System.Net.Http;
    using System.Net.Http.Headers;

    public interface IRequestMessage
    {
        Uri RequestUri { get; }
        HttpRequestHeaders Headers { get; }
        string Url { get; set; }
        string RequestBodyJson { get; set; }
        HttpContent Content { get; set; }
    }
}