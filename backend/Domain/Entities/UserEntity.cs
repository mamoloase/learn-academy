using Domain.Constants;

namespace Domain.Entities;
public class UserEntity : BaseEntity
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Image { get; set; }
    public string Password { get; set; }
    public string Role { get; set; } = RoleConstants.Student;

    public string TeacherId { get; set; }
    public TeacherEntity Teacher { get; set; }

    public List<OrderEntity> Orders { get; set; } = new();
    public List<CourseEntity> Courses { get; set; } = new();
}