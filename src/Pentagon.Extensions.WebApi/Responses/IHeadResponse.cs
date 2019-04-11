namespace Pentagon.Extensions.WebApi.Responses {
    public interface IHeadResponse<THeaders> : IBasicResponse
            where THeaders : IApiResponseHeaders
    {
        THeaders Headers { get; set; }
    }
}