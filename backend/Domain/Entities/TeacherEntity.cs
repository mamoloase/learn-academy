namespace Domain.Entities;
public class TeacherEntity : BaseEntity
{
    public string Image { get; set; }
    public string Description { get; set; }

    public string UserId { get; set; }
    public UserEntity User { get; set; }

    public List<CourseEntity> Courses { get; set; } = new();
}
