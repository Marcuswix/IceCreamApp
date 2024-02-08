using Share.Services;

namespace PresentationIceCreamConsoleApp.Views
{
    public class DeleteCustomerMenu
    {
        private readonly CustomerService _customerService;

        public DeleteCustomerMenu(CustomerService customerService)
        {
            _customerService = customerService;
        }

        public async Task ShowDeleteCustomer()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Delete Menu");
                Console.WriteLine("###########");
                Console.WriteLine("Write the email to delete the account: ");
                Console.WriteLine("Press 'q' to go back to Main Menu");

                var customerToDelete = Console.ReadLine()!;

                if (customerToDelete.ToLower() == "q")
                {
                    break;
                }

                var result = _customerService.DeleteCustomer(customerToDelete);

                if (result == true)
                {
                    Console.Clear();
                    Console.WriteLine("Account successfully deleted. Press any key to continue.");
                    Thread.Sleep(1000);
                    break;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Something went wrong...");
                    Thread.Sleep(1000);
                }
            }
 
        }
    }
}
