namespace RuneSharper.Application.Services.DateTimeProvider;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
    DateTime InstantiatedUtc { get; }
}
