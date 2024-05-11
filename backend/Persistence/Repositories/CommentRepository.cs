
using Domain.Entities;
using Persistence.Contexts;
using Application.Interfaces.Repositories;

namespace Persistence.Repositories;
public class CommentRepository : BaseRepository<CommentEntity>, ICommentRepository
{
    public CommentRepository(ApplicationDBContext context) : base(context)
    {
    }
}
