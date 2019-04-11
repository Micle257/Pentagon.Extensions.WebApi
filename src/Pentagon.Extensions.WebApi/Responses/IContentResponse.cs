namespace Pentagon.Extensions.WebApi.Responses {
    public interface IContentResponse<out TContent> : IBasicResponse
    {
        bool HasValue { get; }

        TContent Value { get; }
    }
}