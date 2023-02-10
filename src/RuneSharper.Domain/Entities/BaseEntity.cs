namespace RuneSharper.Domain.Entities;

public abstract class BaseEntity<T> : BaseEntity
{
    public abstract T Id { get; init; }
}

public class BaseEntity { }
