namespace Pentagon.Extensions.WebApi.Responses {
    using System.Collections.Generic;
    using System.Net.Http.Headers;

    public interface IApiResponseHeaders {
        HttpResponseHeaders BaseHeaders { get; set; }
        IDictionary<string, object> GetApiHeaders();
    }
}