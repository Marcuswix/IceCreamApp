namespace PresentationIceCreamConsoleApp.Views
{
    public class EditOrDeleteProductMenu
    {
        private readonly EditProductMenu editProductMenu;
        private readonly DeleteProductMenu deleteProductMenu;
        private readonly AllProductsMenu allProductsMenu;

        public EditOrDeleteProductMenu(EditProductMenu editProductMenu, DeleteProductMenu deleteProductMenu, AllProductsMenu allProductsMenu)
        {
            this.editProductMenu = editProductMenu;
            this.deleteProductMenu = deleteProductMenu;
            this.allProductsMenu = allProductsMenu;
        }

        public async Task ShowEditOrDeleteMenu()
        {
            bool end = true;
            do
            {
                Console.Clear();
                Console.WriteLine("Handle Products");
                Console.WriteLine("################");
                Console.WriteLine("1. Edit product");
                Console.WriteLine("2. Delete product");
                Console.WriteLine("3. Show all products");
                Console.WriteLine("4. Go back to Main Menu");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await editProductMenu.ShowEditProductMenu();
                        break;
                    case "2":
                        await deleteProductMenu.ShowDeleteProductMenu();
                        break;
                    case "3":
                        await allProductsMenu.ShowAllProductsMenu();
                        break;
                    case "4":
                        end = false;
                        break;
                    default:
                        {
                            Console.WriteLine("Invalid choice");
                            Thread.Sleep(1000);
                            break;
                        }
                }
            } while (end);
        }

    }
}
