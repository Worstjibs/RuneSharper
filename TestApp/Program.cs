using DotnetOsrsApiWrapper;
using RuneSharper.Services.Stats;
using RuneSharper.Shared.Entities;

var service = new OsrsApiService(new PlayerInfoService(new HttpClient()));

var accounts = new List<Character>
{
    new Character { UserName = "worstjibs" },
    new Character { UserName = "groege" },
    new Character { UserName = "idiotgroege" }
};

var results = await service.QueryHiScoresByAccountsAsync(accounts);

Console.WriteLine(results);