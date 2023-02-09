using Azure.Messaging.ServiceBus;
using RuneSharper.Services.Services.SaveStats;

namespace RuneSharper.Worker;

public class RuneSharperMessageWorker : BackgroundService
{
    private readonly ServiceBusClient _serviceBusClient;
    private readonly ILogger<RuneSharperMessageWorker> _logger;
    private readonly IServiceProvider _serviceProvider;

    public RuneSharperMessageWorker(
        ServiceBusClient serviceBusClient,
        ILogger<RuneSharperMessageWorker> logger,
        IServiceProvider serviceProvider)
    {
        _serviceBusClient = serviceBusClient;
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var processor = _serviceBusClient.CreateProcessor("runesharper-queue");

        processor.ProcessMessageAsync += MessageHandler;
        processor.ProcessErrorAsync += ErrorHandler;

        await processor.StartProcessingAsync();
    }

    // handle received messages
    private async Task MessageHandler(ProcessMessageEventArgs args)
    {
        var message = args.Message.Body.ToString();

        _logger.LogInformation(message);

        using var scope = _serviceProvider.CreateScope();

        var saveStatsService = scope.ServiceProvider.GetRequiredService<ISaveStatsService>();

        await saveStatsService.SaveStatsForCharacters(new[] { message });

        // complete the message. message is deleted from the queue. 
        await args.CompleteMessageAsync(args.Message);
    }

    // handle any errors when receiving messages
    private Task ErrorHandler(ProcessErrorEventArgs args)
    {
        Console.WriteLine(args.Exception.ToString());
        return Task.CompletedTask;
    }
}
