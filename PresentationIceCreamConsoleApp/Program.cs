using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PresentationIceCreamConsoleApp.Views;
using Share.Contexts;
using Share.Dtos;
using Share.Repositories;
using Share.Services;

var builder = Host.CreateDefaultBuilder().ConfigureServices(services =>
{
    services.AddDbContext<DataContext>(options =>
        options.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=B:\DataStorageCourse\IceCreamApp\Share\Data\DataBaseIceCreamApp.mdf;Integrated Security=True;Connect Timeout=30"));
    services.AddScoped<CategoryRepository>();
    services.AddScoped<ProductRepository>();
    services.AddScoped<CustomerRepository>();
    services.AddScoped<AddressRepository>();
    services.AddScoped<ProductsService>();
    services.AddScoped<MainMenu>();
    services.AddScoped<AddCustomerMenu>();
    services.AddScoped<AddProductMenu>();
    services.AddScoped<BuyProductMenu>();
    services.AddScoped<CustomerService>();
    services.AddScoped<EditOrDeleteCustomerMenu>();
    services.AddScoped<DeleteCustomerMenu>();
    services.AddScoped<EditCustomer>();
    services.AddScoped<Customer>();
    services.AddScoped<AllCustomersMenu>();
    services.AddScoped<EditOrDeleteProductMenu>();
    services.AddScoped<EditProductMenu>();
    services.AddScoped<DeleteProductMenu>();
    services.AddScoped<AllProductsMenu>();


}).Build();

using (var serviceScope = builder.Services.CreateScope())
{
    var serviceProvider = serviceScope.ServiceProvider;

    var mainMenu = serviceProvider.GetRequiredService<MainMenu>();

    mainMenu.ShowMainMenu();
}

Console.ReadKey();
