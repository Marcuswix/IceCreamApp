using Share.Dtos;
using Share.Repositories;
using Share.Services;

namespace PresentationIceCreamConsoleApp.Views
{
    public class AddProductMenu
    {
        private readonly ProductsService _productsService;
        private readonly ProductRepository _productRepository;

        public AddProductMenu(ProductsService productsService, ProductRepository productRepository)
        {
            _productsService = productsService;
            _productRepository = productRepository;
        }
        public async Task ShowAddProductMenu()
        {
            var product = new Product();

            Console.Clear();
            Console.WriteLine("< Add Product >");
            Console.WriteLine("################");
            Console.WriteLine("1. Article Number: ");
            product.ArticleNumber = Console.ReadLine()!;
            Console.WriteLine("2. Title: ");
            product.Title = Console.ReadLine()!;
            Console.WriteLine("3. Description: ");
            product.Description = Console.ReadLine()!;
            Console.WriteLine("4. Price: ");

            if (decimal.TryParse(Console.ReadLine(), out decimal price))
            {
                product.Price = price;
            }
            else 
            {
                Console.WriteLine("Please enter a valid price (ex. 123)");
            }

            Console.WriteLine("5. Category: ");
            product.CategoryName = Console.ReadLine()!;
            Console.WriteLine("6. Subcategory: ");
            product.SubcategoryName = Console.ReadLine()!;
            Console.WriteLine("7. Manufacturer: ");
            product.ManufacturerName = Console.ReadLine()!;
            Console.WriteLine("8. Image: ");
            product.ImageUrl = Console.ReadLine()!;

            if(product != null)
            {
                bool AddedProduct = _productsService.CreateProduct(product);

                if (AddedProduct == true)
                {
                    Console.Clear();
                    Console.WriteLine("\n-------------------------------");
                    Console.WriteLine("\nProduct was added successfully!");
                    Console.Write("\n\nPress any key to continue...");
                    Console.ReadKey();
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("\n-----------------------");
                    Console.WriteLine("\nSomething went wrong. Please try again.");
                    Console.Write("\n\nPress any key to continue...");
                    Console.ReadKey();
                }
            }

            else
            {
                Console.Clear();
                Console.WriteLine("No product was added");
                Thread.Sleep(1100);
            }
        }
    }
}
