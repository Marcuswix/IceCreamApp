using Share.Services;

namespace PresentationIceCreamConsoleApp.Views
{
    public class BuyProductMenu
    {
        private readonly ProductsService _productsService;
        private readonly OrderService _orderService;
        private readonly CustomerService _customerService;

        public BuyProductMenu(ProductsService productsService, OrderService orderService, CustomerService customerService)
        {
            _productsService = productsService;
            _orderService = orderService;
            _customerService = customerService;
        }

        public async Task ShowBuyProductMenu()
        {
            var products = await _productsService.GetAllProducts();
            
            Console.Clear();
            Console.WriteLine("Please login first to see and order ourer products:");
            Console.WriteLine("###############################################");
            Console.WriteLine("Email:");
            var email = Console.ReadLine();

            if (email != null)
            {
                var customer = await _customerService.GetACustomer(email);

                if (customer != null)
                {
                    Console.Clear();
                    Console.WriteLine("Password:");
                    var password = Console.ReadLine();

                    if (password == customer.Password)
                    {
                        bool end = true;
                        do
                        {
                            Console.Clear();
                            Console.WriteLine($"Welcome {customer.FirstName} {customer.LastName}!");
                            Thread.Sleep(1000);
                            Console.Clear();
                            Console.WriteLine("< Our Products >");

                            if (products != null)
                            {
                                foreach (var p in products)
                                {
                                    Console.WriteLine("--------------------------");
                                    Console.WriteLine("Category: " + p.CategoryName + "\n" + "Subcategory: " + p.SubcategoryName + "\n" + "Title: " + p.Title + "\n" + "Description: " + p.Description + "\n" + "Price: " + p.Price + "\n");
                                }
                            }
                            else
                            {
                                Console.WriteLine("At the moment there is no products for sale...");
                                end = false;
                                Console.Write("\n\nPress any key to continue...");
                                Console.ReadKey();
                            }
                            {
                                Console.WriteLine("Write the Product title of the product you wish to buy:");
                                Console.WriteLine("(Press 'q' to go back)");
                                var product = Console.ReadLine() ?? string.Empty;

                                if (product == "q")
                                {
                                    end = false;
                                }
                                else
                                {
                                    if (product != null)
                                    {
                                        var productToBuy = await _productsService.GetOneProduct(product);

                                        if (productToBuy != null)
                                        {
                                            Console.Clear();
                                            Console.WriteLine("Write the quantity you wish to order:");
                                            int quantity = int.Parse(Console.ReadLine()!);

                                            if (quantity > 0)
                                            {
                                                Console.Clear();
                                                Console.WriteLine("View and confirm your order:");
                                                Console.WriteLine("############################");
                                                Console.WriteLine($"Product Title: {productToBuy}\nQuantity: {quantity}\nTotal price: {productToBuy.Price * quantity} kr.");
                                                Console.WriteLine("\nDo you want to place your order? (y/n)");
                                                var confirm = Console.ReadLine()!;

                                                if (confirm == "y")
                                                {
                                                    await _orderService.AddOrder(productToBuy, customer, quantity);
                                                    Console.Clear();
                                                    Console.WriteLine($"You have now ordered {quantity} {productToBuy.Title}. They will arrive shortly.");
                                                    Console.WriteLine("Press any key to continue.");
                                                }
                                                else
                                                {
                                                    Console.WriteLine("No order was placed.");
                                                    Console.WriteLine("Press any key to continue.");
                                                }
                                            }
                                            else
                                            {
                                                Console.WriteLine("Not a vaild quantity");
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("No product with that title found...");
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("No product with that title found...");
                                    }
                                    Console.ReadKey();
                                }
                            }
                        } while (end);
                    }
                    else 
                    {
                       Console.WriteLine("Incorrect Password");
                        Console.WriteLine("Press any key to continue.");
                        Console.ReadKey();
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("There are no customers with that email. Please register to be able to buy our products.");
                    Console.WriteLine("Press any key to continue.");
                    Console.ReadKey();
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("There are no registered customer with that email... Please register to be able to but our products!");
                Console.WriteLine("Press any key to continue.");
                Console.ReadKey();
            }
        }
    }
}
