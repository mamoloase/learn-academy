namespace Domain.Models;
public class TeacherModel : BaseModel
{
    public string Image { get; set; }
    public string Description { get; set; }

    public string UserId { get; set; }
    public UserModel User { get; set; }
}
