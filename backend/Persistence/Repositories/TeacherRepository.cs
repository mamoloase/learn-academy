using Domain.Entities;
using Persistence.Contexts;
using Application.Interfaces.Repositories;

namespace Persistence.Repositories;
public class TeacherRepository : BaseRepository<TeacherEntity>, ITeacherRepository
{
    public TeacherRepository(ApplicationDBContext context) : base(context)
    {
    }
}
