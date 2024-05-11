namespace Application.Exceptions;
public class AccessDeniedException : Exception
{
    public AccessDeniedException() : base("access deined exception")
    {

    }

    public AccessDeniedException(string message) : base(message)
    {

    }
}
