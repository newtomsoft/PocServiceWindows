namespace PocServiceWindows;

public sealed class WindowsBackgroundService : BackgroundService
{
    public override async Task StartAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            await Job.DoJob();
        }
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        throw new NotImplementedException();
    }
}