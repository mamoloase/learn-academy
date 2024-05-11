using Domain.Entities;
using Persistence.Contexts;
using Application.Interfaces.Repositories;

namespace Persistence.Repositories;
public class OrderRepository : BaseRepository<OrderEntity>, IOrderRepository
{
    public OrderRepository(ApplicationDBContext context) : base(context)
    {
    }
}
