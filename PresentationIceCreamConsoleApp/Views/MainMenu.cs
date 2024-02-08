namespace PresentationIceCreamConsoleApp.Views
{
    public class MainMenu
    {
        private readonly AddProductMenu addProductMenu;
        private readonly AddCustomerMenu addCustomerMenu;
        private readonly BuyProductMenu buyProductMenu;
        private readonly EditOrDeleteCustomerMenu editOrDeleteCustomerMenu;
        private readonly EditOrDeleteProductMenu editOrDeleteProductMenu;
        private readonly HandleOrderMenu handleOrderMenu;

        public MainMenu(AddProductMenu addProductMenu, AddCustomerMenu addCustomerMenu, BuyProductMenu buyProductMenu, EditOrDeleteCustomerMenu editOrDeleteCustomerMenu, EditOrDeleteProductMenu editOrDeleteProductMenu, HandleOrderMenu handleOrderMenu)
        {
            this.addProductMenu = addProductMenu;
            this.addCustomerMenu = addCustomerMenu;
            this.buyProductMenu = buyProductMenu;
            this.editOrDeleteCustomerMenu = editOrDeleteCustomerMenu;
            this.editOrDeleteProductMenu = editOrDeleteProductMenu;
            this.handleOrderMenu = handleOrderMenu;
        }

        bool end = true;

        public async Task ShowMainMenu()
        {
            do
            {
                Console.Clear();
                Console.WriteLine("Main Menu");
                Console.WriteLine("##########");
                Console.WriteLine("1. Create an account");
                Console.WriteLine("2. Handle accounts");
                Console.WriteLine("3. Add a product");
                Console.WriteLine("4. Handle products");
                Console.WriteLine("5. Make a Order");
                Console.WriteLine("6. Handle Orders");
                Console.WriteLine("7. Quit\n");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await addCustomerMenu.AddCustomerMenuShow();
                        break;
                    case "2":
                        await editOrDeleteCustomerMenu.ShowEditOrDeleteCustomerMenu();
                        break;
                    case "3":
                        await addProductMenu.ShowAddProductMenu();
                        break;
                    case "4":
                        await editOrDeleteProductMenu.ShowEditOrDeleteMenu();
                        break;
                    case "5":
                        await buyProductMenu.ShowBuyProductMenu();
                        break;
                    case "6":
                        await handleOrderMenu.HandleOrderMenuShow();
                        break;
                    case "7":
                        AreYouShure();
                        break;
                }
            } while (end);
        }

        public void AreYouShure()
        {
            Console.Clear();
            Console.WriteLine("Are you shure you want to quit? (y/n)");
            var choice = Console.ReadLine().ToLower()!;

            if (choice == "y")
            {
                Console.Clear();
                Console.WriteLine("See you another time!");
                Thread.Sleep(1000);
                end = false;
            }
        }

    }
}
