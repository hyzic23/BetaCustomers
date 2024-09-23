namespace BetaCustomers.API.Models;

public class BaseResponse
{
    public int StatusCode { get; set; }
    public object Message { get; set; }
    //public MessageDTO Message { get; set; }
    
    // Default Constructor
    public BaseResponse()
    {
    }
    
    // Constructor with status code and message
    public BaseResponse(int  statusCode, object message)
    {
        StatusCode = statusCode;
        Message = message;
    }
}

public class MessageDTO
{
    public object Message { get; set; }
    
    // Constructor
    public MessageDTO(object message)
    {
        Message = message;
    }
}