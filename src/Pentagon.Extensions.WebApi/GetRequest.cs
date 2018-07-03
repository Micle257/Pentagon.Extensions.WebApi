// -----------------------------------------------------------------------
//  <copyright file="GetRequest.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.WebApi
{
    using System.Net.Http;

    public abstract class GetRequest<T> : Request<T>
    {
        /// <inheritdoc />
        public override AuthorizationRequirement AuthorizationRequirement => AuthorizationRequirement.NotRequired;

        /// <inheritdoc />
        public override HttpMethod Method => HttpMethod.Get;
    }
}