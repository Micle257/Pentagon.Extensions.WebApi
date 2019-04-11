// -----------------------------------------------------------------------
//  <copyright file="HeadRequest.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.WebApi.Requests
{
    using System.Net.Http;

    public abstract class HeadRequest<T> : Request<T>
    {
        /// <inheritdoc />
        public override HttpMethod Method => HttpMethod.Head;
    }
}