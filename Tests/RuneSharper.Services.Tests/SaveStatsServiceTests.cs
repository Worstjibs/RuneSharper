using Moq;
using RuneSharper.Data.Repositories;
using RuneSharper.Services.SaveStats;
using RuneSharper.Services.Stats;
using RuneSharper.Shared.Entities;
using RuneSharper.Shared.Entities.Snapshots;

namespace RuneSharper.Services.Tests;

public class SaveStatsServiceTests
{
    private SaveStatsService _saveStatsService;

    private Mock<IOsrsApiService> _osrsApiService;
    private Mock<ICharacterRepository> _characterRepository;

    [SetUp]
    public void SetUp()
    {
        _osrsApiService = new Mock<IOsrsApiService>();
        _characterRepository = new Mock<ICharacterRepository>();

        _saveStatsService = new SaveStatsService(_osrsApiService.Object, _characterRepository.Object);
    }

    [Test]
    public async Task SaveStatsForCharacter_GivenAnExistingValidAccount_ShouldAddASnapshotToIt()
    {
        // Arrange
        var accountName = "worstjibs";
        var account = new Character { UserName = "worstjibs" };
        var snapshot = new Snapshot();

        _characterRepository.Setup(x => x.GetCharacterByNameAsync(accountName))
            .ReturnsAsync(account);
        _characterRepository.Setup(x => x.Complete())
            .ReturnsAsync(true);

        _osrsApiService.Setup(x => x.QueryHiScoresByAccountAsync(account))
            .ReturnsAsync(snapshot);

        // Act
        await _saveStatsService.SaveStatsForCharacter(accountName);
        
        // Assert
        Assert.That(account.Snapshots, Is.Not.Empty);
        Assert.That(account.Snapshots.First(), Is.SameAs(snapshot));
        Assert.That(snapshot.Character, Is.SameAs(account));
    }

    [Test]
    public async Task SaveStatsForCharacter_GivenANewValidAccount_ShouldInsertItToTheDatabase()
    {
        // Arrange
        var accountName = "worstjibs";
        var snapshot = new Snapshot();

        _characterRepository.Setup(x => x.Complete())
            .ReturnsAsync(true);

        _osrsApiService
            .Setup(x => x.QueryHiScoresByAccountAsync(It.Is<Character>(c => c.UserName == accountName)))
            .ReturnsAsync(snapshot);

        // Act
        await _saveStatsService.SaveStatsForCharacter(accountName);

        // Assert
        _characterRepository
            .Verify(x => x.Insert(It.Is<Character>(c => 
                c.UserName == accountName && c.Snapshots.First() == snapshot)), Times.Once);
    }

    [Test]
    public async Task SaveStatsForCharacter_GivenAnInvalidAccount_ShouldSetNameChangedToTrue()
    {
        // Arrange
        var accountName = "worstjibs";
        var account = new Character { UserName = "worstjibs" };

        _characterRepository.Setup(x => x.GetCharacterByNameAsync(accountName))
            .ReturnsAsync(account);
        _characterRepository.Setup(x => x.Complete())
            .ReturnsAsync(true);

        // Act
        await _saveStatsService.SaveStatsForCharacter(accountName);

        // Assert
        Assert.That(account.NameChanged, Is.True);
    }
}
