using Domain.Entities;
using Persistence.Contexts;
using Application.Interfaces.Repositories;

namespace Persistence.Repositories;
public class CategoryRepository : BaseRepository<CategoryEntity>, ICategoryRepository
{
    public CategoryRepository(ApplicationDBContext context) : base(context)
    {
    }
}
