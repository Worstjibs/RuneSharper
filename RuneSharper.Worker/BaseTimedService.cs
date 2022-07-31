using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuneSharper.Worker;

public abstract class BaseTimedService : BackgroundService
{
    private PeriodicTimer? _timer;

    protected abstract int Interval { get; }

    public override Task StartAsync(CancellationToken cancellationToken)
    {
        _timer = new PeriodicTimer(TimeSpan.FromSeconds(Interval));

        return base.StartAsync(cancellationToken);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        do
        {
            await DoWorkAsync();
        } while (await _timer!.WaitForNextTickAsync(stoppingToken)
            && !stoppingToken.IsCancellationRequested);
    }

    protected abstract Task DoWorkAsync();
}