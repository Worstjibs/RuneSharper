using RuneSharper.Domain.Entities;
using System.Linq.Expressions;

namespace RuneSharper.Data.Specifications;

internal class CharacterByUserNameSpecification : Specification<Character>
{
    public CharacterByUserNameSpecification(string userName)
        : base(character => character.UserName == userName)
    {
    }

    public CharacterByUserNameSpecification(IEnumerable<string> userNames, bool includeNamedChanged = false)
        : base(character => userNames.Contains(character.UserName) && character.NameChanged == includeNamedChanged)
    {
    }
}
