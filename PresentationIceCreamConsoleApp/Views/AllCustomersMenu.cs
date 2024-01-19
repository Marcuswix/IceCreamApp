using Share.Services;

namespace PresentationIceCreamConsoleApp.Views
{
    public class AllCustomersMenu
    {
        private readonly CustomerService customerService;

        public AllCustomersMenu(CustomerService customerService)
        {
            this.customerService = customerService;
        }

        public void ShowAllCustomersMenu()
        {
            bool end = true;

            do
            {
                Console.Clear();
                Console.WriteLine("Write the password to see all the accounts");
                var password = Console.ReadLine();

                if (password == "Bytmig123!")
                {
                    Console.WriteLine("\nThe password is correct!");
                    Thread.Sleep(1000);
                    end = false;
                }
                else
                {
                    Console.WriteLine("\nIncorrect password, try again.");
                    Thread.Sleep(1000);
                }

            } while (end);

            Console.Clear();
            Console.WriteLine("< All the Accounts >");
            Console.WriteLine("####################\n");

            var customer = customerService.GetAllCustomers();

            if (customer != null)
            {
                Console.Clear();
                Console.WriteLine("< All the Accounts >");
                Console.WriteLine("####################\n");

                foreach (var c in customer)
                {
                    Console.WriteLine($"Id: {c.Id}\nName: {c.FirstName} {c.LastName}\nEmail: {c.Email}\nPassword: {c.Password}\nAddress: {c.SteetName}, {c.PostalCode}, {c.City}\n");
                    Console.WriteLine("##########################\n");
                }
            }
            else
            {
                Console.WriteLine("There are no customer-accounts in the database");
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
