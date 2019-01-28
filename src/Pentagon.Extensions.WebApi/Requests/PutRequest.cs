// -----------------------------------------------------------------------
//  <copyright file="PutRequest.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.WebApi.Requests
{
    using System;
    using System.Net.Http;
    using Interfaces;

    public abstract class PutRequest<TContent, TRequestBody> : Request<TContent>, IHasRequestBody<TRequestBody>
            where TRequestBody : class
    {
        /// <inheritdoc />
        public abstract TRequestBody RequestBody { get; set; }

        /// <inheritdoc />
        public override HttpMethod Method => HttpMethod.Put;

        /// <inheritdoc />
        protected override void ValidateCore(RequestValidationBuilder builder)
        {
            if (RequestBody == null)
                builder.AddError(new ArgumentNullException(nameof(RequestBody)));
        }
    }
}