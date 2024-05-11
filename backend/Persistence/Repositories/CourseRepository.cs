using Domain.Entities;
using Persistence.Contexts;
using Application.Interfaces.Repositories;

namespace Persistence.Repositories;
public class CourseRepository : BaseRepository<CourseEntity>, ICourseRepository
{
    public CourseRepository(ApplicationDBContext context) : base(context)
    {
    }
}
