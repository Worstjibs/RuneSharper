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

    public CharacterByUserNameWithSnapshotsSpecification(IEnumerable<string> userNames, bool includeNamedChanged = false)
        : base(character => userNames.Contains(character.UserName) && character.NameChanged == includeNamedChanged)
    {
        AddInclude(c => c.Snapshots);
    }
}
