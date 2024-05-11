using Application.Interfaces.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;
public class EpisodeRepository : BaseRepository<EpisodeEntity>, IEpisodeRepository
{
    public EpisodeRepository(ApplicationDBContext context) : base(context)
    {
    }
}
