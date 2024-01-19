using Share.Dtos;
using Share.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationIceCreamConsoleApp.Views
{
    public class AddProductMenu
    {
        private readonly ProductsService _productsService;

        public AddProductMenu(ProductsService productsService)
        {
            _productsService = productsService;
        }

        public void ShowAddProductMenu()
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
            product.Price = decimal.Parse(Console.ReadLine()!);
            Console.WriteLine("5. Category: ");
            product.CategoryName = Console.ReadLine()!;
            Console.WriteLine("6. Manufacturer: ");
            product.ManufacturerName = Console.ReadLine()!;

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
                    Console.WriteLine("\nThere already exist a product with the same article Number...");
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
