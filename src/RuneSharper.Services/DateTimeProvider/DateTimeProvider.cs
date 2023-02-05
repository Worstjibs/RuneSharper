namespace RuneSharper.Services.DateTimeProvider;

public class DateTimeProvider : IDateTimeProvider
{
    private DateTime _instantiatedUtc;

    public DateTimeProvider()
    {
        _instantiatedUtc = DateTime.UtcNow;
    }

    public DateTime UtcNow => DateTime.UtcNow;

    public DateTime InstantiatedUtc => _instantiatedUtc;
}
