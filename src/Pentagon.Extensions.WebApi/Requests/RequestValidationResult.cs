namespace Pentagon.Extensions.WebApi.Requests {
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class RequestValidationResult
    {
        public IEnumerable<Exception> Errors { get; } = new List<Exception>();

        public bool IsValid => !Errors.Any();

        public void AddError(Exception exception)
        {
            (Errors as List<Exception>).Add(exception);
        }
    }
}