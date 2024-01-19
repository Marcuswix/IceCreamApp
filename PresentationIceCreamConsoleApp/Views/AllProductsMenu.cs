using Share.Services;

namespace PresentationIceCreamConsoleApp.Views
{
    public class AllProductsMenu
    {
        private readonly ProductsService _productsService;

        public AllProductsMenu(ProductsService productsService)
        {
            _productsService = productsService;
        }

        public void ShowAllProductsMenu()
        {
            var products = _productsService.GetAllProducts();

            Console.Clear();
            Console.WriteLine("All our products");
            Console.WriteLine("#################");

            foreach (var p in products)
            {

                Console.WriteLine($"\nArticle Number: {p.ArticleNumber}\nTitle: {p.Title}\nCategory: {p.CategoryName}\nPrice: {p.Price}\nManufacturer: {p.ManufacturerName}");
                Console.WriteLine("#################\n");
            }

            Console.ReadKey();
        }
    }
}
