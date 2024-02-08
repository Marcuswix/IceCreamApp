using Share.Dtos;
using Share.Services;

namespace PresentationIceCreamConsoleApp.Views
{
    public class DeleteProductMenu
    {
        private readonly ProductsService _productsService;

        public DeleteProductMenu(ProductsService productsService)
        {
            _productsService = productsService;
        }

        public async Task ShowDeleteProductMenu()
        {
            Console.Clear();
            Console.WriteLine("Delete product");
            Console.WriteLine("###############");
            Console.WriteLine("Write the productnumber of the product you wish to delete:");
            var productToDelete = Console.ReadLine()!;

            var result = await _productsService.DeleteProduct(productToDelete);

            if (result == true)
            {
                Console.Clear() ;
                Console.WriteLine("The product was successfully deleted! (Press any key to continue.)");
                Console.ReadKey();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("There exist no product with that article number. (Press any key to continue.)");
                Console.ReadKey();
            }
        }
    }
}
