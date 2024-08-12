namespace API;

#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
public class ApiExceptions(int statusCode, string message, string? details)

{
    public int StatusCode { get; set; } = statusCode;
    public string Message { get; set; } = message;

    public string? Details { get; set; } = details;

}
