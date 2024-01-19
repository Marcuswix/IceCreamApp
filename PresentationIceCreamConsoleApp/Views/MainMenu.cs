namespace PresentationIceCreamConsoleApp.Views
{
    public class MainMenu
    {
        private readonly AddProductMenu addProductMenu;
        private readonly AddCustomerMenu addCustomerMenu;
        private readonly BuyProductMenu buyProductMenu;
        private readonly EditOrDeleteCustomerMenu editOrDeleteCustomerMenu;
        private readonly EditOrDeleteProductMenu editOrDeleteProductMenu;

        public MainMenu(AddProductMenu addProductMenu, AddCustomerMenu addCustomerMenu, BuyProductMenu buyProductMenu, EditOrDeleteCustomerMenu editOrDeleteCustomerMenu, EditOrDeleteProductMenu editOrDeleteProductMenu)
        {
            this.addProductMenu = addProductMenu;
            this.addCustomerMenu = addCustomerMenu;
            this.buyProductMenu = buyProductMenu;
            this.editOrDeleteCustomerMenu = editOrDeleteCustomerMenu;
            this.editOrDeleteProductMenu = editOrDeleteProductMenu;
        }

        bool end = true;

        public void ShowMainMenu()
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
                Console.WriteLine("5. Buy a product");
                Console.WriteLine("6. Quit\n");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        addCustomerMenu.AddCustomerMenuShow();
                        break;
                    case "2":
                        editOrDeleteCustomerMenu.ShowEditOrDeleteCustomerMenu();
                        break;
                    case "3":
                        addProductMenu.ShowAddProductMenu();
                        break;
                    case "4":
                        editOrDeleteProductMenu.ShowEditOrDeleteMenu();
                        break;
                    case "5":
                        buyProductMenu.ShowBuyProductMenu();
                        break;
                    case "6":
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
                Environment.Exit(0);
            }
        }

    }
}
