using RuneSharper.Domain.Entities;
using RuneSharper.Domain.Enums;
using RuneSharper.Domain.Interfaces;
using RuneSharper.Services.Services.SaveStats;
using RuneSharper.Services.Services.Snapshots.ChangeAggregation;
using RuneSharper.Services.Models;
using RuneSharper.Shared.Enums;

namespace RuneSharper.Services.Services.Characters;

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
        var characters = await _characterRepository.GetAllAsync();
        var latestSnapshots = await _snapshotRepository.GetLatestSnapshotsAsync(characters.Select(x => x.UserName));

        var characterModels = characters.Join(
            latestSnapshots,
            c => c.Id,
            s => s.Character.Id,
            (c, s) =>
            {
                var overallSkill = s.Skills.FirstOrDefault(x => x.Type == SkillType.Overall);

                return new CharacterListModel
                {
                    UserName = c.UserName,
                    FirstTracked = c.DateCreated,
                    TotalExperience = overallSkill?.Experience ?? 0,
                    TotalLevel = overallSkill?.Level ?? 0
                };
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

        if (latestSnapshot is null)
            return characterModel;

        characterModel.Stats = new StatsModel(latestSnapshot);
        characterModel.Activities = new ActivitiesModel(latestSnapshot.Activities);

        characterModel.ActivitiesChange = await _activitiesChangeAggregationHandler.GetChangeAggregationsForUser(userName);
        characterModel.StatsChange = await _statsChangeAggregationHandler.GetChangeAggregationsForUser(userName);

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
