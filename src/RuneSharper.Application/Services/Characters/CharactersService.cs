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
    private readonly ICharacterRepository _characterRepository;
    private readonly ISnapshotRepository _snapshotRepository;
    private readonly ISaveStatsService _saveStatsService;
    private readonly IProjectedCharacterRepository<CharacterListModel> _projectedCharacterRepository;
    private readonly IChangeAggregationHandler<ActivitiesChangeModel> _activitiesChangeAggregationHandler;
    private readonly IChangeAggregationHandler<StatsChangeModel> _statsChangeAggregationHandler;

    public CharactersService(
        ICharacterRepository characterRepository,
        ISnapshotRepository snapshotRepository,
        ISaveStatsService saveStatsService,
        IProjectedCharacterRepository<CharacterListModel> projectedCharacterRepository,
        IChangeAggregationHandler<ActivitiesChangeModel> activitiesChangeAggregationHandler,
        IChangeAggregationHandler<StatsChangeModel> statsChangeAggregationHandler)
    {
        _characterRepository = characterRepository;
        _snapshotRepository = snapshotRepository;
        _saveStatsService = saveStatsService;
        _projectedCharacterRepository = projectedCharacterRepository;
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

    public async Task<IEnumerable<CharacterListModel>> GetCharacterListModelsAsync(string? sort, SortDirection? direction)
    {
        return await _projectedCharacterRepository.GetProjectedCharacters(GetProperty(sort), direction);
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

    private static Expression<Func<CharacterListModel, object>>? GetProperty(string? sort)
    {
        if (sort is null)
            return null;

        var property = typeof(CharacterListModel)
            .GetProperty(sort, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.IgnoreCase);

        if (property == null)
            throw new ArgumentException($"Sort expression {sort} is invalid");

        if (property.GetCustomAttribute<UnsortableAttribute>() is not null)
            throw new ArgumentException($"Property {property.Name} is unsortable");

        var parameter = Expression.Parameter(typeof(CharacterListModel));
        var propertyExpression = Expression.Property(parameter, property);
        var conversion = Expression.Convert(propertyExpression, typeof(object));
        return Expression.Lambda<Func<CharacterListModel, object>>(conversion, parameter);
    }
}
