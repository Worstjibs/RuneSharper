using FluentAssertions;
using RuneSharper.Shared.Models;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace RuneSharper.API.Integration.Tests.Characters;

[Collection("Shared Collection")]
public class ChracterControllerTests : IClassFixture<RuneSharperApiFactory>
{
    private readonly RuneSharperApiFactory _waf;
    private readonly HttpClient _client;

    public ChracterControllerTests(RuneSharperApiFactory waf)
    {
        _waf = waf;
        _client = _waf.HttpClient;
    }

    [Fact]
    public async Task GetCharacter_ShouldReturnAnExistingCharacter()
    {
        // Arrange
        var characterName = "worstjibs";

        // Act
        var response = await _client.GetAsync($"api/character/{characterName}");
        var characterModel = await response.Content.ReadFromJsonAsync<CharacterViewModel>();

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        characterModel.Should().NotBeNull();
        characterModel!.UserName.Should().Be(characterName);
    }
}