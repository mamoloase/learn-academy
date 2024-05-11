namespace Domain.Entities;
public class EpisodeEntity : BaseEntity
{
    public string Name { get; set; }
    public string Document { get; set; }

    public string CourseId { get; set; }
    public CourseEntity Course { get; set; }
}