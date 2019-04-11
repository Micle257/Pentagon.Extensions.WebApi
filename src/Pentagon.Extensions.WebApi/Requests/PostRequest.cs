// -----------------------------------------------------------------------
//  <copyright file="PostRequest.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.WebApi.Requests
{
    using System;
    using System.Net.Http;
    using Interfaces;

    public abstract class PostRequest<TContent, TRequestBody> : Request<TContent>, IRequestWithBody<TRequestBody>
            where TRequestBody : class
    {
        /// <inheritdoc />
        public override HttpMethod Method => HttpMethod.Post;

        /// <inheritdoc />
        public TRequestBody RequestBody { get; set; }

        /// <inheritdoc />
        object IRequestWithBody.RequestBody
        {
            get => RequestBody;
            set => RequestBody = (TRequestBody) value;
        }

        /// <inheritdoc />
        protected override void ValidateCore(RequestValidationBuilder builder)
        {
            if (RequestBody == null)
                builder.AddError(new ArgumentNullException(nameof(RequestBody)));
        }
    }
}