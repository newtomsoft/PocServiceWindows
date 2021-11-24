Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<WindowsBackgroundService>();
    })
    .UseWindowsService()
    .Build().Run();

