
namespace Domain.Models;
public class CategoryModel : BaseModel
{
    public string Name { get; set; }
    public string Description { get; set; }

    public string ParentId { get; set; }
}
