using RuneSharper.Domain.Entities;
using RuneSharper.Shared.Enums;
using System.Linq.Expressions;

namespace RuneSharper.Domain.Interfaces;

public interface IProjectedCharacterRepository<T> : IRepository<Character>, ICharacterRepository
{
    Task<IEnumerable<T>> GetProjectedCharacters(string? orderBy, SortDirection? direction);
}
