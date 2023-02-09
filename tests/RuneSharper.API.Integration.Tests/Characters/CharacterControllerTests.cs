using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualBasic;
using RuneSharper.Data;
using RuneSharper.Data.Seed;
using RuneSharper.Services.Models;

namespace RuneSharper.API.Integration.Tests.Characters;

[Collection("Shared collection")]
public class CharacterControllerTests : BaseIntegrationTest
{
    public CharacterControllerTests(RuneSharperApiFactory waf) : base(waf) { }

    [Fact]
    public async Task GetViewModel_ShouldReturnAnExistingCharacterViewModel()
    {
        // Arrange
        await SeedDb();
        var characterName = "worstjibs";

        // Act
        var response = await _client.GetAsync($"api/character/{characterName}");
        var characterModel = await response.Content.ReadFromJsonAsync<CharacterViewModel>();

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        characterModel.Should().NotBeNull();
        characterModel!.UserName.Should().Be(characterName);
    }

    [Fact]
    public async Task GetViewModel_ShouldReturnBadRequest_WhenCharacterIsNotFound()
    {
        // Arrange
        var characterName = "worstjibs";

        // Act
        var response = await _client.GetAsync($"api/character/{characterName}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    private async Task SeedDb()
    {
        using var scope = _waf.Services.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<RuneSharperContext>();

        await Seed.SeedDataAsync(context);
    }
}