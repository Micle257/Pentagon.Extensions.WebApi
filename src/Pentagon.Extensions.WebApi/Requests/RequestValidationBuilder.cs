namespace Pentagon.Extensions.WebApi.Requests {
    using System;

    public class RequestValidationBuilder
    {
        RequestValidationResult _result = new RequestValidationResult();

        public RequestValidationResult Build()
        {
            return _result;
        }

        public RequestValidationBuilder AddError(Exception exception)
        {
            _result.AddError(exception);
            return this;
        }
    }
}