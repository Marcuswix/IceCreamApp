using Microsoft.Extensions.Hosting;
using System.Windows;
using Share.Contexts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using IceCreamAppWPF.ViewModels;
using IceCreamAppWPF.Views;
using Share.Services;
using Share.Dtos;
using Share.Entities;
using Share.Repositories;

namespace IceCreamAppWPF
{
    public partial class App : Application
    {
        private IHost _builder;

        public App()
        {
            _builder = Host.CreateDefaultBuilder().ConfigureServices(services => {
            
                services.AddDbContext<DataContext>(x => x.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=B:\DataStorageCourse\IceCreamApp\Share\Data\DataBaseIceCreamApp.mdf;Integrated Security=True"));
                services.AddDbContext<DataContextOrders>(options =>
                        options.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=B:\DataStorageCourse\IceCreamApp\Share\Data\DataBaseFirstIceCreamApp.mdf;Integrated Security=True"));
                services.AddScoped<ProductRepository>();
                services.AddScoped<CustomerRepository>();
                services.AddScoped<AddressRepository>();
                services.AddScoped<ProductsService>();
                services.AddScoped<ProductEntity>();
                services.AddScoped<CustomerService>();
                services.AddScoped<Customer>();
                services.AddScoped<Product>();
                services.AddScoped<Order>();
                services.AddScoped<OrderRepository>();
                services.AddScoped<OrderDto>();
                services.AddScoped<OrderService>();
                services.AddScoped<MainWindow>();
                services.AddScoped<MainViewModel>();
                services.AddScoped<UserListViewModel>();
                services.AddTransient<ProductListViewModel>();
                services.AddScoped<ProductPageView>();
                services.AddScoped<UserPageView>();
                services.AddScoped<WelcomePage>();
                services.AddScoped<BuyProductsView>();
                services.AddScoped<BuyProductsViewModel>();
                services.AddScoped<CustomerService>();
            }).Build();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _builder.Start();

            var mainWindow = _builder!.Services.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }
    }

}
