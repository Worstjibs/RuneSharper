﻿namespace RuneSharper.Application.Models;

public class CharacterListModel
{
    public string UserName { get; set; } = string.Empty;
    public int TotalLevel { get; set; }
    public int TotalExperience { get; set; }
    public DateTime FirstTracked { get; set; }
    public string HighestSkill { get; set; } = string.Empty;
}
