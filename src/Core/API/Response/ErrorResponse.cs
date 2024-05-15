namespace Core.Response;
public record ErrorResponse(int StatusCode, IDictionary<string, string[]>? Messages = null) : ActionResponse(StatusCode);