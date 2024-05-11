using Application.Interfaces.Repositories;

namespace Application.Interfaces;
public interface IUnitOfWork
{
    public IUserRepository User { get; }
    public ICourseRepository Course { get; }
    public ITeacherRepository Teacher { get; }
    public ICommentRepository Comment { get; }
    public ICategoryRepository Category { get; }
    public IArticleRepository Article { get; }
    public IOrderRepository Order { get; }
    public IEpisodeRepository Episode { get; }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

}
