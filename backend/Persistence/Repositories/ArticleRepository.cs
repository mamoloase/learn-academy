using Domain.Entities;
using Application.Interfaces.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;
public class ArticleRepository : BaseRepository<ArticleEntity>, IArticleRepository
{
    public ArticleRepository(ApplicationDBContext context) : base(context)
    {

    }
}
