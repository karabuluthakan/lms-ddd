#pragma warning disable CS8618
#pragma warning disable CS8602
namespace Domain.SeedWork;

public abstract class Entity : Entity<Guid>
{
    protected Entity(Guid id) : base(id)
    {
    }

    protected Entity() : base(Guid.NewGuid())
    {
    }
}

public abstract class Entity<TKey> : IEntity<TKey>
{
    public virtual TKey Id { get; protected set; }
    public virtual DateTimeOffset CreatedAt { get; protected set; }

    protected Entity(TKey id)
    {
        Id = Guard.Against.Null(id);
        CreatedAt = new DateTimeOffset(DateTime.Now);
    }

    protected Entity()
    {
    }

    private int? _requestedHashCode;

    public bool IsTransient()
    {
        return Id.Equals(default(TKey));
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Entity<TKey> item)
            return false;

        if (ReferenceEquals(this, item))
            return true;

        if (GetType() != item.GetType())
            return false;

        if (item.IsTransient() || IsTransient())
            return false;
        return item == this;
    }


    public override int GetHashCode()
    {
        if (!IsTransient())
        {
            _requestedHashCode ??= Id.GetHashCode() ^ 31;
            return _requestedHashCode.Value;
        }

        return base.GetHashCode();
    }

    public static bool operator ==(Entity<TKey> left, Entity<TKey> right)
    {
        if (Equals(left, null))
            return Equals(right, null);
        return left.Equals(right);
    }

    public static bool operator !=(Entity<TKey> left, Entity<TKey> right)
    {
        return !(left == right);
    }

    protected static void CheckRule(IBusinessRule rule)
    {
        if (rule.IsBroken())
        {
            throw new BusinessRuleException(rule);
        }
    }
}