using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RuneSharper.Application.Extensions;
using RuneSharper.Data.Specifications;
using RuneSharper.Domain.Entities;
using RuneSharper.Domain.Entities.Snapshots;
using RuneSharper.Domain.Enums;
using RuneSharper.Domain.Interfaces;
using RuneSharper.Shared.Enums;

namespace RuneSharper.Data.Repositories;

public class CharacterRepository : Repository<Character>, ICharacterRepository
{
    public CharacterRepository(RuneSharperContext context, IRuneSharperConnectionFactory connectionFactory)
        : base(context, connectionFactory)
    {
    }

    public async Task<Character?> GetCharacterByNameAsync(string userName)
    {
        return await ApplySpecification(new CharacterByUserNameSpecification(userName))
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Character>> GetCharactersByNameAsync(IEnumerable<string> userNames, bool includeNameChanged = false)
    {
        return await ApplySpecification(new CharacterByUserNameSpecification(userNames))
            .ToListAsync();
    }

    public async Task<IEnumerable<Character>> GetCharactersAsync(string? sortTable, string? sortColumn, SortDirection direction, int skip, int take)
    {
        using var connection = _connectionFactory.GetConnection();

        var sql =
            $"""
            WITH OrderedSnapshots AS (
                SELECT S.Id, S.CharacterId, S.DateCreated 
                FROM
                (
            	    SELECT S.*, ROW_NUMBER() OVER (PARTITION BY CharacterId
            									    ORDER BY DateCreated DESC) RN
            	    FROM Snapshots S
                ) S
                WHERE S.RN = 1
            )
            
            SELECT Characters.*, Snapshots.*, SkillSnapshot.*
            FROM OrderedSnapshots Snapshots
            INNER JOIN SkillSnapshot
                ON Snapshots.Id = SkillSnapshot.SnapshotId
                AND SkillSnapshot.Type = 0
            INNER JOIN Characters ON Snapshots.CharacterId = Characters.Id 
            """;

        if (!sortTable.IsNullOrEmpty() && !sortColumn.IsNullOrEmpty())
            sql += $"ORDER BY {sortTable}.{sortColumn} {direction.GetDisplayName()};";

        return await connection.QueryAsync<Character, Snapshot, SkillSnapshot, Character>(
            sql,
            (character, snapshot, skillSnaphot) =>
            {
                snapshot.Skills = new List<SkillSnapshot>
                {
                    skillSnaphot
                };

                character.Snapshots = new List<Snapshot>
                {
                    snapshot
                };

                return character;
            });
    }
}
