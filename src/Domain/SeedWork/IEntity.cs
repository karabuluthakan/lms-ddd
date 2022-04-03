namespace Domain.SeedWork;

public interface IEntity
{
}

public interface IEntity<TKey> : IEntity
{
    public TKey Id { get; }
    public DateTimeOffset CreatedAt { get; } 
}