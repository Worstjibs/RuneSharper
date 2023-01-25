namespace RuneSharper.Shared.Entities;

public class BaseIntEntity : BaseEntity<int>
{
    public override int Id { get; init; }
}
