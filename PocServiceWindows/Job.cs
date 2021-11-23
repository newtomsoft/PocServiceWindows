namespace PocServiceWindows;

public sealed class Job
{
    public static async Task DoJob1()
    {
        const string pathFile = "c:\\PocServiceWindows";
        var timer = new PeriodicTimer(TimeSpan.FromSeconds(5));
        while (await timer.WaitForNextTickAsync())
        {
            var fullPathFile = Path.Combine(pathFile, Guid.NewGuid() + ".txt");
            await using var writer = new StreamWriter(fullPathFile);
            writer.WriteLine(DateTime.Now);
        }
    }

    public static async Task DoJob2()
    {
        const string pathFile = "c:\\PocServiceWindows";
        var timer = new PeriodicTimer(TimeSpan.FromSeconds(5));
        while (await timer.WaitForNextTickAsync())
        {
            var fullPathFile = Path.Combine(pathFile, DateTime.UtcNow.ToString("o").Replace(':','_') + ".txt");
            await using var writer = new StreamWriter(fullPathFile);
            writer.WriteLine(DateTime.Now);
        }
    }
}