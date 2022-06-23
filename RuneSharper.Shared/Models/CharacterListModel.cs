using RuneSharper.Shared.Entities.Snapshots;
using RuneSharper.Shared.Enums;

namespace RuneSharper.Shared.Models
{
    public class CharacterListModel
    {
        public string UserName { get; set; } = default!;
        public CharacterType Type { get; set; } = default!;
        public StatsModel Stats { get; set; } = default!;
    }
}
