using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuneSharper.Shared.Models;

public class CharacterViewModel
{
    public string UserName { get; set; } = default!;
    public DateTime FirstTracked { get; set; }
    public StatsModel? Stats { get; set; }
}
