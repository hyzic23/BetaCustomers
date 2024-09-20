
namespace BetaCustomers.API.Models;

public class BaseResponse
{
    public BaseResponse(bool isSuccess, string? statusCode, string message)
    {
        IsSuccess = isSuccess;
        StatusCode = statusCode;
        Message = message;
    }
    
    public BaseResponse(bool isSuccess, string message)
    {
        IsSuccess = isSuccess;
        Message = message;
    }
    
    public BaseResponse()
    {
        IsSuccess = true;
        StatusCode = string.Empty;
        Message = string.Empty;
    }

    public bool IsSuccess { get; set; }
    public string? StatusCode { get; set; }
    public string Message { get; set; }
}