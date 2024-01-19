using Share.Services;

namespace PresentationIceCreamConsoleApp.Views
{
    public class BuyProductMenu
    {
        private readonly ProductsService _productsService;

        public BuyProductMenu(ProductsService productsService)
        {
            _productsService = productsService;
        }

        public void ShowBuyProductMenu()
        {
            var products = _productsService.GetAllProducts();
            
            Console.Clear();

            Console.WriteLine("< Our Products >");

            if (products != null)
            {
                foreach(var p in products)
                {
                    Console.WriteLine("--------------------------");
                    Console.WriteLine("Category: " + p.CategoryName + "\n" + "Title: " + p.Title + "\n" + "Description: " + p.Description + "\n" + "Price: " + p.Price + "\n");
                }
            }
            else
            {
                Console.WriteLine("At the moment there is no products for sale...");
                Console.Write("\n\nPress any key to continue...");
                Console.ReadKey();
            }
            Console.ReadKey();
        }
    }
}
