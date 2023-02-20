using RuneSharper.Domain.Entities;
using RuneSharper.Shared.Enums;
using System.Linq.Expressions;

namespace RuneSharper.Domain.Interfaces;

public interface IProjectedCharacterRepository<T> : IRepository<Character>
{
    Task<IEnumerable<T>> GetProjectedCharacters(Expression<Func<T, object>>? orderBy, SortDirection? direction);
}
