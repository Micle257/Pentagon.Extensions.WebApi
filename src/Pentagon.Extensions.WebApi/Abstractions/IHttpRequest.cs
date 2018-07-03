// -----------------------------------------------------------------------
//  <copyright file="IHttpRequest.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.WebApi.Abstractions
{
    using System.Net.Http;

    /// <summary> Represents a http request. </summary>
    public interface IHttpRequest
    {
        /// <summary> Gets the method of the request. </summary>
        /// <value> The http method. </value>
        HttpMethod Method { get; }
    }
}