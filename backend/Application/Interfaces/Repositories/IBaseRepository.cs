using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;

namespace Application.Interfaces.Repositories;
public interface IBaseRepository<EntityType>
{
    public Task InsertAsync(EntityType entity, CancellationToken cancellationToken = default);
    public Task InsertAsync(IEnumerable<EntityType> entities, CancellationToken cancellationToken = default);

    public Task UpdateAsync(EntityType entity, CancellationToken cancellationToken = default);
    public Task UpdateAsync(IEnumerable<EntityType> entities, CancellationToken cancellationToken = default);
    public Task DeleteAsync(EntityType entity, CancellationToken cancellationToken = default);
    public Task DeleteAsync(IEnumerable<EntityType> entities, CancellationToken cancellationToken = default);

    public Task<EntityType> GetAsync(
          Expression<Func<EntityType, bool>> predicate = null,
          Func<IQueryable<EntityType>, IIncludableQueryable<EntityType, object>> include = null,
          CancellationToken cancellationToken = default);
    public Task<int> CountAsync(CancellationToken cancellationToken = default);
    public Task<int> CountAsync(Expression<Func<EntityType, bool>> predicate, CancellationToken cancellationToken = default);
    public IQueryable<EntityType> GetQueryable(
        Expression<Func<EntityType, bool>> predicate = null,
        Func<IQueryable<EntityType>,
        IIncludableQueryable<EntityType, object>> include = null);

    public IQueryable<EntityType> FilterAsync(Expression<Func<EntityType, bool>> predicate = null,
        Func<IQueryable<EntityType>, IIncludableQueryable<EntityType, object>> include = null,
        Func<IQueryable<EntityType>, IOrderedQueryable<EntityType>> orderBy = null,
        int? skip = null, int? take = null, CancellationToken cancellationToken = default);

}
