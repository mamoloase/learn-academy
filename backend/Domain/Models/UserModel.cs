namespace Domain.Models;
public class UserModel : BaseModel
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Image { get; set; }
    public string Role { get; set; }

    public string TeacherId { get; set; }
    public TeacherModel Teacher { get; set; }

}
