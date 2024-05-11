
using Domain.Enums;

namespace Domain.Entities;
public class CourseEntity : BaseEntity
{
    public string Title { get; set; }
    public string Description { get; set; }
    
    public string Image { get; set; }
    public string Video { get; set; }

    public decimal Price { get; set; }

    public CourseLevel Level { get; set; }
    public CourseStatus Status { get; set; }

    public string TeacherId { get; set; }
    public TeacherEntity Teacher { get; set; }

    public string CategoryId { get; set; }
    public CategoryEntity Category { get; set; }

    public List<CommentEntity> Comments { get; set; } = new();
    public List<EpisodeEntity> Episodes { get; set; } = new();

}
