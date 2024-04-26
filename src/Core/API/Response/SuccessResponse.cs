namespace Core.Response;
public record SuccessResponse<T>(int StatusCode, T Data) : ActionResponse(StatusCode);