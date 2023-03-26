
namespace Application.Exceptions;

public class CodeErrorException
{
    public int StatusCode { get; set; }
    public string? Message { get; set; }
    public string? Details { get; set; }
    public CodeErrorException(int statusCode, string? message = null, string? details = null)
    {
        StatusCode = statusCode;
        Message = message;
        Details = details;
    }
}
