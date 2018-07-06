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
        public ApiException(ApiExceptionArguments arguments, string message, Exception exception) : base(message, exception)
        {
            Arguments = arguments;
        }
        
        public ApiExceptionArguments Arguments { get; }
    }
}