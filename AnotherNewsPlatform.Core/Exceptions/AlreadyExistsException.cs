namespace AnotherNewsPlatform.Core.Exceptions;

public class AlreadyExistsException: Exception
{ 
    public AlreadyExistsException(string message) : base(message)
    {

    }

    public AlreadyExistsException(string entityName, object identifier) : base($"{entityName} with identifier {identifier} already exists.")
    {

    }
}