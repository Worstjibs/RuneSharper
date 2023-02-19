#nullable disable
using RuneSharper.Application.Extensions;
using RuneSharper.Shared.Enums;

namespace RuneSharper.Application.Models;

public class FrequencyModel
{
    public FrequencyModel()
    {
    }

    public FrequencyModel(FrequencyType frequency)
    {
        Type = frequency;
        Name = frequency.GetDisplayName();
    }

    public FrequencyType Type { get; }
    public string Name { get; }
}
