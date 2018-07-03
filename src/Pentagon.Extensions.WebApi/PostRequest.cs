// -----------------------------------------------------------------------
//  <copyright file="PostRequest.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.WebApi
{
    using System;
    using System.Net.Http;
    using Abstractions;

    public abstract class PostRequest<TContent, TRequestBody> : Request<TContent>, IPostRequest<TContent, TRequestBody>
            where TRequestBody : class
    {
        /// <inheritdoc />
        public abstract TRequestBody RequestBody { get; set; }

        /// <inheritdoc />
        public override HttpMethod Method => HttpMethod.Post;

        /// <inheritdoc />
        public override void Validate()
        {
            if (RequestBody == null)
                throw new ArgumentNullException(nameof(RequestBody));
        }
    }
}