using Domain.Enums;

namespace Domain.Models;
public class OrderModel : BaseModel
{
    public OrderStatus Status { get; set; }

    public string UserId { get; set; }
    public UserModel User { get; set; }

    public string CourseId { get; set; }
    public CourseModel Course { get; set; }
}
