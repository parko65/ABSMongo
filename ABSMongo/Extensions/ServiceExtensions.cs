using Contracts;
using LoggerService;
using Microsoft.Extensions.Configuration;
using Repository;
using Service;
using Service.Contracts;
using System.Reflection;

namespace ABSMongo.Extensions;
public static class ServiceExtensions
{
    public static void ConfigureLoggerService(this IServiceCollection services) =>
        services.AddSingleton<ILoggerManager, LoggerManager>();

    public static void SetupConfiguration(this MauiAppBuilder builder)
    {
        var assembly = Assembly.GetExecutingAssembly();
        using Stream? stream = assembly.GetManifestResourceStream("ABSMongo.appsettings.json")
            ?? throw new FileNotFoundException("appsettings.json not found in the assembly manifest resources.");
        var config = new ConfigurationBuilder()
            .AddJsonStream(stream)
            .Build();

        builder.Configuration.AddConfiguration(config);
    }

    public static void ConfigureRepositoryManager(this IServiceCollection services) =>
        services.AddSingleton<IRepositoryManager, RepositoryManager>();

    public static void ConfigureServiceManager(this IServiceCollection services) =>
        services.AddSingleton<IServiceManager, ServiceManager>();
}
