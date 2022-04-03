namespace Domain.DataAccess.Persistence;

public interface IRepository<T,TKey> where T : Entity<TKey>, new()
{
    ValueTask<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken = default);

    ValueTask<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default);

    ValueTask<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>>? predicate = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        string? includeString = null,
        bool disableTracking = true, CancellationToken cancellationToken = default);

    ValueTask<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>>? predicate = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        List<Expression<Func<T, object>>>? includes = null,
        bool disableTracking = true, CancellationToken cancellationToken = default);

    ValueTask<IReadOnlyList<T>> GetAsync(ISpecification<T> spec, CancellationToken cancellationToken = default);
    ValueTask<T> GetByIdAsync(TKey id, CancellationToken cancellationToken = default);
    ValueTask<T> AddAsync(T entity, CancellationToken cancellationToken = default);
    ValueTask UpdateAsync(T entity, CancellationToken cancellationToken = default);
    ValueTask DeleteAsync(T entity, CancellationToken cancellationToken = default);
    ValueTask<int> CountAsync(ISpecification<T> spec, CancellationToken cancellationToken = default);
}