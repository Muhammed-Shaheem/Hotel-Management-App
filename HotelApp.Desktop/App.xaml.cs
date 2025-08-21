using HotelAppLibrary.Data;
using HotelAppLibrary.Databases;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Windows;

namespace HotelApp.Desktop;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
 
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var services = new ServiceCollection();
        services.AddTransient<IDataAccess, SqlDataAccess>();
        services.AddTransient<IDatabaseData, SqlData>();    
       
        services.AddTransient<MainWindow>();

        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");
        IConfiguration builder = config.Build();

        services.AddSingleton(builder);
        var serviceProvider = services.BuildServiceProvider();
        var mainWindow = serviceProvider.GetService<MainWindow>();

        mainWindow?.Show();





     

    }
}

