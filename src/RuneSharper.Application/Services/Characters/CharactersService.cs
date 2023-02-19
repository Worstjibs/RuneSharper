using RuneSharper.Domain.Entities;
using RuneSharper.Domain.Enums;
using RuneSharper.Domain.Interfaces;
using RuneSharper.Application.Services.SaveStats;
using RuneSharper.Application.Services.Snapshots.ChangeAggregation;
using RuneSharper.Application.Models;
using RuneSharper.Shared.Enums;
using System.Reflection;
using RuneSharper.Application.Attributes;

namespace RuneSharper.Application.Services.Characters;

public class CharactersService : ICharactersService
{
    private readonly ICharacterRepository _characterRepository;
    private readonly ISnapshotRepository _snapshotRepository;
    private readonly ISaveStatsService _saveStatsService;
    private readonly IChangeAggregationHandler<ActivitiesChangeModel> _activitiesChangeAggregationHandler;
    private readonly IChangeAggregationHandler<StatsChangeModel> _statsChangeAggregationHandler;

    public CharactersService(
        ICharacterRepository characterRepository,
        ISnapshotRepository snapshotRepository,
        ISaveStatsService saveStatsService,
        IChangeAggregationHandler<ActivitiesChangeModel> activitiesChangeAggregationHandler,
        IChangeAggregationHandler<StatsChangeModel> statsChangeAggregationHandler)
    {
        _characterRepository = characterRepository;
        _snapshotRepository = snapshotRepository;
        _saveStatsService = saveStatsService;
        _activitiesChangeAggregationHandler = activitiesChangeAggregationHandler;
        _statsChangeAggregationHandler = statsChangeAggregationHandler;
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
        string? tableName = null;
        string? columnName = null;

        if (sort is not null)
            (tableName, columnName) = GetProperty(sort);

        var characters = await _characterRepository.GetCharactersAsync(tableName, columnName, direction, 0, 0);

        return characters.Select(x => new CharacterListModel
        {
            UserName = x.UserName,
            FirstTracked = x.DateCreated,
            TotalExperience = x.Snapshots.First().Skills.First().Experience,
            TotalLevel = x.Snapshots.First().Skills.First().Level
        }).ToList();
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

        if (latestSnapshot is null)
            return characterModel;

        characterModel.Stats = new StatsModel(latestSnapshot);
        characterModel.Activities = new ActivitiesModel(latestSnapshot.Activities);

        characterModel.ActivitiesChange = await _activitiesChangeAggregationHandler.GetChangeAggregationsForUser(userName);
        characterModel.StatsChange = await _statsChangeAggregationHandler.GetChangeAggregationsForUser(userName);

        return characterModel;
    }

    private static (string TableName, string ColumnName) GetProperty(string sort)
    {
        var property = typeof(CharacterListModel)
            .GetProperty(sort, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.IgnoreCase);

        if (property == null)
            throw new ArgumentException($"Sort expression {sort} is invalid");

        var sortMapping = property.GetCustomAttribute<SortMappingAttribute>();

        if (sortMapping == null)
            throw new ArgumentException($"Property {property.Name} does not have sort mapping attribute defined");

        return (sortMapping.TableName, sortMapping.ColumnName);
    }
}
