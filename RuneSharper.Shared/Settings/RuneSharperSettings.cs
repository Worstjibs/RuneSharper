using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuneSharper.Shared.Settings;

public class RuneSharperSettings
{
    public int OsrsApiPollingTime { get; init; }
    public string[] CharacterNames { get; set; } = Array.Empty<string>();
}
