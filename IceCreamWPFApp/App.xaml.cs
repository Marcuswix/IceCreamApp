using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Share.Contexts;
using Share.Repositories;
using Share.Services;
using System.Configuration;
using System.Data;
using System.Net;
using System.Windows;

namespace IceCreamWPFApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IHost builder;

        public App()
        {
            builder = Host.CreateDefaultBuilder().ConfigureServices(services =>
            {
                services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=B:\DataStorageCourse\IceCreamApp\Share\Data\DataBaseIceCreamApp.mdf;Integrated Security=True;Connect Timeout=30"));
                services.AddScoped<CategoryRepository>();
                services.AddScoped<ProductRepository>();
                services.AddScoped<ProductsService>();
                services.AddSingleton<MainWindow>();

            }).Build();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            builder.Start();

            var mainWindow = builder.Services.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }
    }

}
