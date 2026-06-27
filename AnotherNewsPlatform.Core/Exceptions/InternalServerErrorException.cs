namespace AnotherNewsPlatform.Core.Exceptions;

public class InternalServerErrorException : Exception
{
    public InternalServerErrorException(string message): base(message)
    {
        
    }
    
    public InternalServerErrorException(string message, Exception exception): base(message, exception)
    {
        
    }
}