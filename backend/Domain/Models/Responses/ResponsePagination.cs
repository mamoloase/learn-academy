namespace Domain.Models.Responses;
public class ResponsePagination: Response
{
    public int CountPage { get; set; }
    public int CountTotal { get; set; }

    public ResponsePagination() : base(true)
    {

    }
}
