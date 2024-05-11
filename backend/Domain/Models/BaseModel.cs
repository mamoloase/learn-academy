using Domain.Enums;

namespace Domain.Models;
public class BaseModel
{
    public string Id { get; set; }
    public DateTime DateCreationAt { get; set; }
    public DateTime DateModifiedAt { get; set; }
}
