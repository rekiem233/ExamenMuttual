using ExamenMuttual.Shared;
using ExamenMuttual.Shared.Abstraction;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ExamenMuttual
{

    public static class Startup
    {
        public static App Init(Action<HostBuilderContext, IServiceCollection> nativeConfigureServices)
        {
            var systemDir = FileSystem.CacheDirectory;
            Utils.ExtractSaveResource("ExamenMuttual.Shared.appsettings.json", systemDir);
            var fullConfig = Path.Combine(systemDir, "ExamenMuttual.Shared.appsettings.json");

            var host = new HostBuilder()
                            .ConfigureHostConfiguration(c =>
                            {
                                c.AddCommandLine(new string[] { $"ContentRoot={FileSystem.AppDataDirectory}" });
                                c.AddJsonFile(fullConfig);
                            })
                            .ConfigureServices((c, x) =>
                            {
                                nativeConfigureServices(c, x);
                                ConfigureServices(c, x);
                            })
                            .ConfigureLogging(l => l.AddConsole(o =>
                            {
                                o.DisableColors = true;
                            }))
                            .Build();

            App.ServiceProvider = host.Services;

            return App.ServiceProvider.GetService<App>();
        }


        static void ConfigureServices(HostBuilderContext ctx, IServiceCollection services)
        {
            if (ctx.HostingEnvironment.IsDevelopment())
            {
                var world = ctx.Configuration["Hello"];
            }

            services.AddHttpClient();
            services.AddTransient<IMainViewModel, MainViewModel>();
            services.AddTransient<Views.MainPage>();
            services.AddTransient<ISQLite, Shared.Helpers.SQLiteHelper>();
            services.AddSingleton<App>();
        }

    }

}

