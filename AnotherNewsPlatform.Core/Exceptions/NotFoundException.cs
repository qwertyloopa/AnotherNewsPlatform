namespace AnotherNewsPlatform.Core.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string message): base(message)
    {
        
    }

    public NotFoundException(string? entityName, object identifier) : base($"Entity {entityName} with identifier: {identifier} was not found")
    {
        
    }
}