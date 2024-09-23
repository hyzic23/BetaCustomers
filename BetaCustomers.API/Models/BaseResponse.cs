namespace BetaCustomers.API.Models;

public class BaseResponse
{
    public int StatusCode { get; set; }
    public MessageDTO Message { get; set; }
    
    // Default Constructor
    public BaseResponse()
    {
    }
    
    // Constructor with status code and message
    public BaseResponse(int  statusCode, MessageDTO message)
    {
        StatusCode = statusCode;
        Message = message;;
    }
}

public class MessageDTO
{
    public string Message { get; set; }
    
    // Constructor
    public MessageDTO(string message)
    {
        Message = message;
    }
}