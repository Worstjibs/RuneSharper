using RuneSharper.Shared.Entities;
using System.Linq.Expressions;

namespace RuneSharper.Data.Specifications;

internal class CharacterByUserNameWithSnapshotsSpecification : Specification<Character>
{
    public CharacterByUserNameWithSnapshotsSpecification(string userName)
        : base(character => character.UserName == userName)
    {
        AddInclude(c => c.Snapshots);
    }

    public CharacterByUserNameWithSnapshotsSpecification(IEnumerable<string> userNames)
        : base(character => userNames.Contains(character.UserName))
    {
        AddInclude(c => c.Snapshots);
    }
}
