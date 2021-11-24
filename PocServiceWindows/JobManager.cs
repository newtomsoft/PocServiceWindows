using Cnr.Shared.EventScheduler;

namespace PocServiceWindows;

public sealed class JobManager
{
    public void DoJob1(object sender, SchedulerEventArgs schedulerEventArgs)
    {
        const string pathFile = "c:\\PocServiceWindows";
        var fullPathFile = Path.Combine(pathFile, "asyncCnrScheduler.txt");
        using var writer = new StreamWriter(fullPathFile, true);
        writer.WriteLineAsync("*** Counter : xxx ***");
        writer.WriteLineAsync($"time 1 :  {TimeOnly.FromDateTime(DateTime.Now):s.fff}");
        Thread.Sleep(1000);
        writer.WriteLineAsync($"time 2 :  {TimeOnly.FromDateTime(DateTime.Now):s.fff}");
        Thread.Sleep(1000);
        writer.WriteLineAsync($"time 3 :  {TimeOnly.FromDateTime(DateTime.Now):s.fff}");
        Thread.Sleep(1000);
        writer.WriteLineAsync($"time 4 :  {TimeOnly.FromDateTime(DateTime.Now):s.fff}");
        Thread.Sleep(1000);
        writer.WriteLineAsync($"time 5 :  {TimeOnly.FromDateTime(DateTime.Now):s.fff}");
    }

    public async Task DoJob2(CancellationToken cancellationToken)
    {
        var counter = 0;
        var timer = new PeriodicTimer(TimeSpan.FromMilliseconds(500));
        while (await timer.WaitForNextTickAsync(cancellationToken))
        {
            counter++;
            const string pathFile = "c:\\PocServiceWindows";
            var fullPathFile = Path.Combine(pathFile, "async.txt");
            await using var writer = new StreamWriter(fullPathFile, true);
            await writer.WriteLineAsync($"*** Counter : {counter} ***");
            await writer.WriteLineAsync($"time 1 :  {TimeOnly.FromDateTime(DateTime.Now):s.fff}");
            await Task.Delay(1000);
            await writer.WriteLineAsync($"time 2 :  {TimeOnly.FromDateTime(DateTime.Now):s.fff}");
            await Task.Delay(1000);
            await writer.WriteLineAsync($"time 3 :  {TimeOnly.FromDateTime(DateTime.Now):s.fff}");
            await Task.Delay(1000);
            await writer.WriteLineAsync($"time 4 :  {TimeOnly.FromDateTime(DateTime.Now):s.fff}");
            await Task.Delay(1000, cancellationToken); // no waiting if cancelled
            await writer.WriteLineAsync($"time 5 :  {TimeOnly.FromDateTime(DateTime.Now):s.fff}");
        }
    }
}
