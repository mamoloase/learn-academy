namespace Domain.Models;
public class CommentModel : BaseModel
{
    public string Message { get; set; }
    public string AnswerId { get; set; }

    public string UserId { get; set; }
    public UserModel User { get; set; }
}
