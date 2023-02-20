using RuneSharper.Domain.Entities;
using RuneSharper.Domain.Interfaces;
using RuneSharper.Application.Services.SaveStats;
using RuneSharper.Application.Services.Snapshots.ChangeAggregation;
using RuneSharper.Application.Models;
using RuneSharper.Shared.Enums;
using System.Reflection;
using System.Linq.Expressions;
using RuneSharper.Application.Attributes;

namespace RuneSharper.Application.Services.Characters;

public class CharactersService : ICharactersService
{
    private readonly ISnapshotRepository _snapshotRepository;
    private readonly ISaveStatsService _saveStatsService;
    private readonly IProjectedCharacterRepository<CharacterListModel> _projectedCharacterRepository;
    private readonly IChangeAggregationHandler<ActivitiesChangeModel> _activitiesChangeAggregationHandler;
    private readonly IChangeAggregationHandler<StatsChangeModel> _statsChangeAggregationHandler;

    public CharactersService(
        IProjectedCharacterRepository<CharacterListModel> projectedCharacterRepository,
        ISnapshotRepository snapshotRepository,
        ISaveStatsService saveStatsService,
        IChangeAggregationHandler<ActivitiesChangeModel> activitiesChangeAggregationHandler,
        IChangeAggregationHandler<StatsChangeModel> statsChangeAggregationHandler)
    {
        _projectedCharacterRepository = projectedCharacterRepository;
        _snapshotRepository = snapshotRepository;
        _saveStatsService = saveStatsService;
        _activitiesChangeAggregationHandler = activitiesChangeAggregationHandler;
        _statsChangeAggregationHandler = statsChangeAggregationHandler;
    }

    public async Task<Character?> GetCharacterAsync(string userName)
    {
        return await _projectedCharacterRepository.GetCharacterByNameAsync(userName);
    }

    public async Task<Character?> UpdateCharacterStatsAsync(string userName)
    {
        // NOTE: This should create message on Azure Service Bus
        // I need to write a RuneSharper Message Service to wrap this properly
        return await _saveStatsService.SaveStatsForCharacter(userName);
    }

    public async Task<IEnumerable<CharacterListModel>> GetCharacterListModelsAsync(string? sort, SortDirection? direction)
    {
        return await _projectedCharacterRepository.GetProjectedCharacters(sort, direction);
    }

    public async Task<CharacterViewModel?> GetCharacterViewModelAsync(string userName)
    {
        var character = await _projectedCharacterRepository.GetCharacterByNameAsync(userName);

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
}
