// -----------------------------------------------------------------------
//  <copyright file="RequestValidationBuilder.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.WebApi.Requests
{
    using System;

    public class RequestValidationBuilder
    {
        readonly RequestValidationResult _result = new RequestValidationResult();

        public RequestValidationResult Build() => _result;

        public RequestValidationBuilder AddError(Exception exception)
        {
            _result.AddError(exception);
            return this;
        }
    }
}