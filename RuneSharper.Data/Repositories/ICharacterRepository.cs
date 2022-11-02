using RuneSharper.Shared.Entities;
using RuneSharper.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuneSharper.Data.Repositories;

public interface ICharacterRepository : IRepository<Character>
{
    Task<Character?> GetCharacterByNameAsync(string userName);
    Task<IEnumerable<Character>> GetCharactersByNameAsync(IEnumerable<string> userNames);
}
