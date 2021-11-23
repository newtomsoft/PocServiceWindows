namespace PocServiceWindows;

public sealed class WindowsBackgroundService : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var jobs = new List<Func<Task>> { Job.DoJob1, Job.DoJob2 };
        await Parallel.ForEachAsync(jobs, stoppingToken, async (job, _) => await job.Invoke());
    }
}