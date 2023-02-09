namespace RuneSharper.Shared.Settings;

public class RuneSharperSettings
{
    public int OsrsApiPollingTime { get; init; }
    public string[] CharacterNames { get; set; } = Array.Empty<string>();
}
