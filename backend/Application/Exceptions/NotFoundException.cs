namespace Application.Exceptions;
public class NotFoundException : Exception
{
    public NotFoundException() : base("can not found object")
    {

    }
    public NotFoundException(string message) : base(message)
    {

    }
}
