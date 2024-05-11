using Domain.Enums;

namespace Domain.Entities;
public class OrderEntity : BaseEntity
{
    public OrderStatus Status { get; set; } = OrderStatus.Pending;

    public string UserId { get; set; }
    public UserEntity User { get; set; }

    public string CourseId { get; set; }
    public CourseEntity Course { get; set; }
}
