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
        writer.WriteLineAsync($"time 1 :  {TimeOnly.FromDateTime(DateTime.Now):H:m:s.fff}");
        Thread.Sleep(10_000);
        writer.WriteLineAsync($"time 2 :  {TimeOnly.FromDateTime(DateTime.Now):H:m:s.fff}");
        Thread.Sleep(10_000);
        writer.WriteLineAsync($"time 3 :  {TimeOnly.FromDateTime(DateTime.Now):H:m:s.fff}");
        Thread.Sleep(10_000);
        writer.WriteLineAsync($"time 4 :  {TimeOnly.FromDateTime(DateTime.Now):H:m:s.fff}");
        Thread.Sleep(10_000);
        writer.WriteLineAsync($"time 5 :  {TimeOnly.FromDateTime(DateTime.Now):H:m:s.fff}");
    }

    public static async Task DoJob2(DateTime startDateTime, DateTime endDateTime, CancellationToken cancellationToken)
    {
        const string filePath = "c:\\PocServiceWindows";
        const string fileName = "async.txt";
        var fullFileName = Path.Combine(filePath, fileName);
        var counter = 0;
        var timer = new PeriodicTimer(TimeSpan.FromMilliseconds(1_000));
        while (await timer.WaitForNextTickAsync(cancellationToken))
        {
            var now = DateTime.Now;
            if (now < startDateTime) continue;
            if (now > endDateTime) return;
            counter++;
            await using var writer = new StreamWriter(fullFileName, true);
            await writer.WriteLineAsync($"*** Counter : {counter} ***");
            for (var iteration = 0; iteration < 100; iteration++) await WriteIteration(writer, iteration);
        }
    }

    private static async Task WriteIteration(TextWriter writer, int iteration)
    {
        await writer.WriteLineAsync($"iteration {iteration} : {DateTime.Now:H:m:s.fff}");
        await Task.Delay(1_000);
        await writer.FlushAsync();
    }
}
