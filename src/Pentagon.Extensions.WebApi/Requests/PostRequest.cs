// -----------------------------------------------------------------------
//  <copyright file="PostRequest.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.WebApi.Requests
{
    using System;
    using System.Net.Http;

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