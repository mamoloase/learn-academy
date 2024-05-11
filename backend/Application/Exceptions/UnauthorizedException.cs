namespace Application.Exceptions;
public class UnauthorizedException : Exception
{

    public UnauthorizedException() : base("unauthorized exception")
    {

    }
    public UnauthorizedException(string message) : base(message)
    {

    }
}
