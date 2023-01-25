﻿using RuneSharper.Shared.Entities;
using RuneSharper.Shared.Enums;
using RuneSharper.Shared.Models;

namespace RuneSharper.Services.Characters;

public interface ICharactersService
{
    Task<Character?> GetCharacterAsync(string userName);
    Task<Character?> UpdateCharacterStatsAsync(string userName);
    Task<IEnumerable<CharacterListModel>> GetCharacterListModelsAsync(string? sort, SortDirection direction);
    Task<CharacterViewModel?> GetCharacterViewModelAsync(string userName);
}