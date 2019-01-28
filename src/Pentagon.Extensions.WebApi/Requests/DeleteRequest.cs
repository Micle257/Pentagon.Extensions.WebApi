// -----------------------------------------------------------------------
//  <copyright file="DeleteRequest.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.WebApi.Requests
{
    using System.Net.Http;

    public abstract class DeleteRequest<T> : Request<T>
    {
        /// <inheritdoc />
        public override AuthorizationRequirement AuthorizationRequirement => AuthorizationRequirement.NotRequired;

        /// <inheritdoc />
        public override HttpMethod Method => HttpMethod.Delete;
    }
}