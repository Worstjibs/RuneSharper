using Xunit;

namespace RuneSharper.API.Integration.Tests;

[CollectionDefinition("Shared collection")]
public class SharedTestCollection : ICollectionFixture<RuneSharperApiFactory>
{
}
