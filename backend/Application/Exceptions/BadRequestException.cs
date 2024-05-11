namespace Application.Exceptions;
public class BadRequestException : Exception
{
    public BadRequestException() : base("data is not valid")
    {

    }
    public BadRequestException(string message) : base(message)
    {

    }
}
