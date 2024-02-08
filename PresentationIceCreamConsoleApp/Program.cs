using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PresentationIceCreamConsoleApp.Views;
using Share.Contexts;
using Share.Dtos;
using Share.Entities;
using Share.Repositories;
using Share.Services;

var builder = Host.CreateDefaultBuilder().ConfigureServices(services =>
{
services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=B:\DataStorageCourse\IceCreamApp\Share\Data\DataBaseIceCreamApp.mdf;Integrated Security=True;Connect Timeout=30"));
services.AddDbContext<DataContextOrders>(options =>
    options.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=B:\DataStorageCourse\IceCreamApp\Share\Data\DataBaseFirstIceCreamApp.mdf;Integrated Security=True"));
services.AddScoped<ProductRepository>();
services.AddScoped<CustomerRepository>();
services.AddScoped<AddressRepository>();
services.AddScoped<ProductsService>();
services.AddScoped<ProductEntity>();
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
services.AddScoped<Product>();
services.AddScoped<Order>();
services.AddScoped<OrderRepository>();
services.AddScoped<OrderDto>();
services.AddScoped<OrderService>();
services.AddScoped<HandleOrderMenu>();
services.AddScoped<DeleteOrderMenu>();
services.AddScoped<EditOrderMenu>();
services.AddScoped<AllOrdersMenu>();

}).Build();

using (var serviceScope = builder.Services.CreateScope())
{
    var serviceProvider = serviceScope.ServiceProvider;

    var mainMenu = serviceProvider.GetRequiredService<MainMenu>();

    await mainMenu.ShowMainMenu();
}
