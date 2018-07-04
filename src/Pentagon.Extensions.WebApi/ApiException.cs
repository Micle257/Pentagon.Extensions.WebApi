// -----------------------------------------------------------------------
//  <copyright file="ApiException.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.WebApi
{
    using System;

    public class ApiException : Exception
    {
        public ApiException(ApiExceptionArguments arguments, string message) : base(message)
        {
            Arguments = arguments;
        }

        public ApiException(ApiExceptionArguments arguments) : this(arguments, arguments.ServerReasonPhrase) { }

        public ApiExceptionArguments Arguments { get; }
    }
}