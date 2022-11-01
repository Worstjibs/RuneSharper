﻿using RuneSharper.Data.Repositories;
using RuneSharper.Services.SaveStats;
using RuneSharper.Shared.Entities;
using RuneSharper.Shared.Enums;
using RuneSharper.Shared.Models;

namespace RuneSharper.Services.Characters;

public class CharactersService : ICharactersService
{
    private readonly ICharacterRepository _characterRepository;
    private readonly ISnapshotRepository _snapshotRepository;
    private readonly ISaveStatsService _saveStatsService;

    public CharactersService(
        ICharacterRepository characterRepository,
        ISnapshotRepository snapshotRepository,
        ISaveStatsService saveStatsService)
    {
        _characterRepository = characterRepository;
        _snapshotRepository = snapshotRepository;
        _saveStatsService = saveStatsService;
    }

    public async Task<Character?> GetCharacterAsync(string userName)
    {
        return await _characterRepository.GetCharacterByNameAsync(userName);
    }

    public async Task<Character?> UpdateCharacterStatsAsync(string userName)
    {
        // NOTE: This should create message on Azure Service Bus
        // I need to write a RuneSharper Message Service to wrap this properly
        return await _saveStatsService.SaveStatsForCharacter(userName);
    }

    public async Task<IEnumerable<CharacterListModel>> GetCharacterListModelsAsync(string? sort, SortDirection direction)
    {
        var characters = await _characterRepository.GetAllAsync();
        var latestSnapshots = await _snapshotRepository.GetLatestSnapshotByCharacter(characters.Select(x => x.UserName));

        var characterModels = characters.Select(x => new CharacterListModel
        {
            UserName = x.UserName,
            TotalExperience = latestSnapshots[x.UserName].Skills.First(x => x.Type == SkillType.Overall).Experience,
            TotalLevel = latestSnapshots[x.UserName].Skills.First(x => x.Type == SkillType.Overall).Level,
            FirstTracked = x.DateCreated
        });

        if (!string.IsNullOrEmpty(sort))
        {
            characterModels = direction == SortDirection.Ascending
                ? characterModels.OrderBy(x => GetProperty(sort, x))
                : characterModels.OrderByDescending(x => GetProperty(sort, x));
        }

        return characterModels;
    }

    public async Task<CharacterViewModel?> GetCharacterViewModelAsync(string userName)
    {
        var character = await _characterRepository.GetCharacterByNameAsync(userName);

        if (character is null)
            return null;

        var latestSnapshot = await _snapshotRepository.GetLatestSnapshotAsync(userName);

        var characterModel = new CharacterViewModel
        {
            UserName = character.UserName,
            FirstTracked = character.DateCreated
        };

        if (latestSnapshot is not null)
            characterModel.Stats = new StatsModel(latestSnapshot);

        return characterModel;
    }

    private static object GetProperty(string sort, CharacterListModel model)
    {
        sort = $"{char.ToUpperInvariant(sort[0])}{sort[1..]}";

        var property = typeof(CharacterListModel).GetProperty(sort);

        if (property != null)
        {
            return property.GetValue(model)!;
        }

        throw new ArgumentException($"Sort expression {sort} is invalid");
    }
}
