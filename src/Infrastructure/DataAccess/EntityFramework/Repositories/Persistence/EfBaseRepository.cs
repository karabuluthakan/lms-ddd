#pragma warning disable CS8603
namespace Infrastructure.DataAccess.EntityFramework.Repositories.Persistence;

/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class EfBaseRepository<T, TKey> : IRepository<T, TKey> where T : Entity<TKey>, new()
{
    protected readonly IDbContextFactory<LmsDbContext> _dbContextFactory;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dbContextFactory"></param>
    protected EfBaseRepository(IDbContextFactory<LmsDbContext> dbContextFactory)
    {
        _dbContextFactory = Guard.Against.Null(dbContextFactory);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual async ValueTask<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        await using (var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken))
        {
            return await dbContext.Set<T>().AsNoTracking().ToListAsync(cancellationToken);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual async ValueTask<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        await using (var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken))
        {
            return await dbContext.Set<T>().Where(predicate).AsNoTracking().ToListAsync(cancellationToken);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="orderBy"></param>
    /// <param name="includeString"></param>
    /// <param name="disableTracking"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual async ValueTask<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>>? predicate = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string? includeString = null,
        bool disableTracking = true, CancellationToken cancellationToken = default)
    {
        await using (var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken))
        {
            IQueryable<T> query = dbContext.Set<T>();
            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (!string.IsNullOrWhiteSpace(includeString))
            {
                query = query.Include(includeString);
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync(cancellationToken);
            }

            return await query.ToListAsync(cancellationToken);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="orderBy"></param>
    /// <param name="includes"></param>
    /// <param name="disableTracking"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual async ValueTask<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>>? predicate = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, List<Expression<Func<T, object>>>? includes = null,
        bool disableTracking = true, CancellationToken cancellationToken = default)
    {
        await using (var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken))
        {
            IQueryable<T> query = dbContext.Set<T>();
            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync(cancellationToken);
            }

            return await query.ToListAsync(cancellationToken);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="spec"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual async ValueTask<IReadOnlyList<T>> GetAsync(ISpecification<T> spec,
        CancellationToken cancellationToken = default)
    {
        await using (var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken))
        {
            return await dbContext.Set<T>().AsQueryable().AsNoTracking()
                .ApplySpecification<T, TKey>(spec)
                .ToListAsync(cancellationToken);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual async ValueTask<T> GetByIdAsync(TKey id, CancellationToken cancellationToken = default)
    {
        await using (var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken))
        {
            return await dbContext.Set<T>().FindAsync(id);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual async ValueTask<T> AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        await using (var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken))
        {
            dbContext.Entry(entity).State = EntityState.Added;
            await dbContext.SaveChangesAsync(cancellationToken);
            return entity;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="cancellationToken"></param>
    public virtual async ValueTask UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        await using (var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken))
        {
            dbContext.Entry(entity).State = EntityState.Modified;
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="cancellationToken"></param>
    public virtual async ValueTask DeleteAsync(T entity, CancellationToken cancellationToken = default)
    {
        await using (var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken))
        {
            dbContext.Entry(entity).State = EntityState.Deleted;
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="spec"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual async ValueTask<int> CountAsync(ISpecification<T> spec,
        CancellationToken cancellationToken = default)
    {
        await using (var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken))
        {
            return await dbContext.Set<T>().AsQueryable().AsNoTracking()
                .ApplySpecification<T, TKey>(spec)
                .CountAsync(cancellationToken);
        }
    }
}