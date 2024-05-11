using Domain.Entities;
using Persistence.Contexts;
using Application.Interfaces.Repositories;

namespace Persistence.Repositories;
public class UserRepository : BaseRepository<UserEntity>, IUserRepository
{
    public UserRepository(ApplicationDBContext context) : base(context)
    {
    }
}
