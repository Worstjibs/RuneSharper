namespace RuneSharper.Services.Models;

public class CharacterListModel
{
    public string UserName { get; set; } = default!;
    public int TotalLevel { get; set; }
    public int TotalExperience { get; set; }
    public DateTime FirstTracked { get; set; }
}
