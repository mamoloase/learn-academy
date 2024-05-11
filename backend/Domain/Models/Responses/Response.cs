namespace Domain.Models.Responses;
public class Response
{
    public bool Status { get; set; }
    public object Result { get; set; }

    public Response(bool status)
    {
        Status = status;
    }
}
