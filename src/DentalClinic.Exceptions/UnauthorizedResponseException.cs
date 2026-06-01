namespace DentalClinic.Exceptions;

public class UnauthorizedResponseException : MessageException
{
    public UnauthorizedResponseException(string message) : base(message)
    {
    }

    public UnauthorizedResponseException() : base("Unauthorized access")
    {
        
    }

}
