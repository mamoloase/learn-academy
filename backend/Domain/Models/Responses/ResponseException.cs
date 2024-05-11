namespace Domain.Models.Responses;
public class ResponseException : Response
{
    public string Message { get; set; }
    public object Exceotions { get; set; }

    public ResponseException() : base(false)
    {

    }
}
