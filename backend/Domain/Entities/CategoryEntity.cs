namespace Domain.Entities;
public class CategoryEntity : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }

    public string ParentId { get; set; }
    public CategoryEntity Parent { get; set; }

    public List<CourseEntity> Courses { get; set; } = new();
    public List<CategoryEntity> Categories { get; set; } = new();
}
