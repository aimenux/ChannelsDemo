using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using App.Examples;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace App;

public static class Program
{
    public static async Task Main()
    {
        var environment = Environment.GetEnvironmentVariable("ENVIRONMENT") ?? "DEV";

        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();

        var services = new ServiceCollection();
        services.AddTransient<IExample, Example1>();
        services.AddTransient<IExample, Example2>();
        services.AddTransient<IExample, Example3>();
        services.AddTransient<IExample, Example4>();
        services.AddTransient<IExample, Example5>();
        services.AddTransient<IExample, Example6>();

        services.Configure<Settings>(configuration.GetSection(nameof(Settings)));

        var serviceProvider = services.BuildServiceProvider();
        var examples = serviceProvider.GetServices<IExample>().ToList();
        var options = serviceProvider.GetService<IOptions<Settings>>();
        var settings = options.Value;

        Console.WriteLine($"Configuration used for '{examples.Count}' examples:");
        Console.WriteLine($"- Items nbr = {settings.ItemsNumber} items");
        Console.WriteLine($"- Producer delay = {settings.ProducerDelay} ms");
        Console.WriteLine($"- Consumer delay = {settings.ConsumerDelay} ms");
        Console.WriteLine($"- Bounded channel size = {settings.BoundedChannelSize}");
        Console.WriteLine();

        foreach (var example in examples)
        {
            var sw = new Stopwatch();
            sw.Start();
            var name = example.GetType().Name;
            var description = example.Description;
            ConsoleColor.Magenta.WriteLine($"{name} -> {description}");
            await example.RunAsync();
            sw.Stop();
            ConsoleColor.Yellow.WriteLine($"Elapsed time: {sw.ElapsedMilliseconds} ms\n");
        }

        Console.WriteLine("Press any key to exit !");
        Console.ReadKey();
    }
}