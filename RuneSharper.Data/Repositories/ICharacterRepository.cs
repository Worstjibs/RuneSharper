using RuneSharper.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuneSharper.Data.Repositories
{
    public interface ICharacterRepository : IRepository<Character>
    {
        Task<Character?> GetCharacterByNameAsync(string accountName);
        Task<IEnumerable<Character?>> GetCharactersByNameAsync(IEnumerable<string> characterNames);
    }
}
