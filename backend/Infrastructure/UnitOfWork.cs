using Application.Interfaces;
using Application.Interfaces.Repositories;

using Persistence.Contexts;
using Persistence.Repositories;

namespace Infrastructure;
public class UnitOfWork : IUnitOfWork
{
    public readonly ApplicationDBContext _context;
    public UnitOfWork(ApplicationDBContext context)
    {
        _context = context;
    }
    public IUserRepository User => new UserRepository(_context);
    public ITeacherRepository Teacher => new TeacherRepository(_context);
    public ICourseRepository Course => new CourseRepository(_context);
    public ICommentRepository Comment => new CommentRepository(_context);
    public ICategoryRepository Category => new CategoryRepository(_context);
    public IArticleRepository Article => new ArticleRepository(_context);
    public IOrderRepository Order => new OrderRepository(_context);
    public IEpisodeRepository Episode => new EpisodeRepository(_context);


    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _context.SaveChangesAsync();
    }
}