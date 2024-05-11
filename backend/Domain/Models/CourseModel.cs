using Domain.Enums;

namespace Domain.Models;
public class CourseModel : BaseModel
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string Image { get; set; }
    public string Video { get; set; }

    public decimal Price { get; set; }

    public CourseLevel Level { get; set; }
    public CourseStatus Status { get; set; }

    public string TeacherId { get; set; }
    public TeacherModel Teacher { get; set; }

    public string CategoryId { get; set; }
}
