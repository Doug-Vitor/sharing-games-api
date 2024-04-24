namespace Core.Response;
public record ErrorResponse(IDictionary<string, string[]> Messages);