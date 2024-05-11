namespace Domain.Entities;
public class CommentEntity : BaseEntity
{
    public string Message { get; set; }

    public string AnswerId { get; set; }
    public CommentEntity Answer { get; set; }

    public string UserId { get; set; }
    public UserEntity User { get; set; }

    public string CourseId { get; set; }
    public CourseEntity Course { get; set; }
}
