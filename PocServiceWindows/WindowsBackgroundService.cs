using Cnr.Shared.EventScheduler;

namespace PocServiceWindows;

public sealed class WindowsBackgroundService : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        //LunchCnrScheduler();
        await LunchFrameworkScheduler(stoppingToken);
    }


    private static void LunchCnrScheduler()
    {
        var jobManager = new JobManager();
        var scheduler = new Scheduler();

        var job1Schedule = new IntervalSchedule("job1", DateTime.Now, 50);
        job1Schedule.Triggered += jobManager.DoJob1;
        scheduler.AddSchedule(job1Schedule);
    }

    private static async Task LunchFrameworkScheduler(CancellationToken stoppingToken)
    {
        var startDateTime = DateTime.Now.AddSeconds(5);
        var endDateTime = startDateTime.AddHours(1);
        var jobs = new List<Func<CancellationToken, Task>> { cancellationToken => JobManager.DoJob2(startDateTime, endDateTime, cancellationToken) };

        await Parallel.ForEachAsync(jobs, stoppingToken, async (job, _) => await job.Invoke(stoppingToken));
    }
}