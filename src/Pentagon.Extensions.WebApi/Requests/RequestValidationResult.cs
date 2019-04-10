// -----------------------------------------------------------------------
//  <copyright file="RequestValidationResult.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.WebApi.Requests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using JetBrains.Annotations;

    public class RequestValidationResult
    {
        [NotNull]
        List<Exception> _errors = new List<Exception>();

        [NotNull]
        public IEnumerable<Exception> Errors => _errors;

        public bool IsValid => !Errors.Any();

        public void AddError(Exception exception)
        {
            _errors.Add(exception);
        }
    }
}