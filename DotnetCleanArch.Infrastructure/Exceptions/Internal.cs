namespace DotnetCleanArch.Infrastructure.Exceptions;

public class InternalException: Exception
{
    
    public InternalException(string message) : base(message)
    {
    }
}