using Share.Entities;

namespace PresentationIceCreamConsoleApp.Views
{
    public class HandleOrderMenu
    {
        private readonly DeleteOrderMenu _deleteOrderMenu;
        private readonly EditOrderMenu _editOrderMenu;
        private readonly AllOrdersMenu _allOrdersMenu;

        public HandleOrderMenu(DeleteOrderMenu deleteOrderMenu, EditOrderMenu editOrderMenu, AllOrdersMenu allOrdersMenu)
        {
            _deleteOrderMenu = deleteOrderMenu;
            _editOrderMenu = editOrderMenu;
            _allOrdersMenu = allOrdersMenu;
        }

        public async Task HandleOrderMenuShow()
        {
            Console.Clear();
            Console.WriteLine("Write admin-password(Bytmig123!) to handle orders.");
            var adminPassword = Console.ReadLine();

            if(adminPassword == "Bytmig123!")
            {
                bool end = true;
                do
                {
                    Console.Clear();
                    Console.WriteLine("Handle Orders: ");
                    Console.WriteLine("################");
                    Console.WriteLine("1. Delete Order");
                    Console.WriteLine("2. Edit Order");
                    Console.WriteLine("3. Show All Orders ");
                    Console.WriteLine("4. Back to Main Menu");

                    var chocie = Console.ReadLine();

                    switch (chocie)
                    {
                        case "1":
                            await _deleteOrderMenu.DeleteOrderMenuShow();
                            break;
                        case "2":
                            await _editOrderMenu.EditOrderMenuShow();
                            break;
                        case "3":
                            Console.Clear();
                            await _allOrdersMenu.AllOrdersMenuShow();
 
                            break;
                        case "4":
                            end = false;
                            break;
                        default:
                            break;
                    }
                } while (end);
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Incorrect password");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }
    }
}
