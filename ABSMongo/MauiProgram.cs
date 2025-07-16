using ABSMongo.Extensions;
using ABSMongo.ServiceTimers;
using Contracts;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.FluentUI.AspNetCore.Components;
using NLog;
using Repository;

namespace ABSMongo
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            LogManager.Setup().LoadConfigurationFromFile(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddFluentUIComponents();

            builder.Services.ConfigureLoggerService();
            builder.SetupConfiguration();
            builder.Services.ConfigureRepositoryManager();
            builder.Services.ConfigureServiceManager();

            builder.Services.AddSingleton<IMongoContext, MongoContext>();

            builder.Services.AddAutoMapper(typeof(MauiProgram));

            builder.Services.AddSingleton<BackgroundTask>(sp => new BackgroundTask(TimeSpan.FromMilliseconds(1000)));

#if DEBUG
            var env = "Development";
#else
        var env = "Production";
#endif

            builder.Services.AddSingleton<IHostEnvironment>(sp =>
            new HostingEnvironment
            {
                EnvironmentName = env, // Or get from config
                ApplicationName = AppDomain.CurrentDomain.FriendlyName
            });

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
