namespace Domain.Models.Requests;
public class RequestPagination
{
    public int Page { get; set; } = 1;

    private int _size = 50;
    public int Size
    {
        get => _size;
        set => _size = Math.Min(value, _size);
    }
}
