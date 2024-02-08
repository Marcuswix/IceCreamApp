using Share.Dtos;
using Share.Services;

namespace PresentationIceCreamConsoleApp.Views
{
    public class AddCustomerMenu
    {
        private readonly CustomerService _customerService;

        public AddCustomerMenu(CustomerService customerService)
        {
            _customerService = customerService;
        }

        public async Task AddCustomerMenuShow()
        {
            var customer = new Customer();

            {
                Console.Clear();
                Console.WriteLine("< Add Customer >");
                Console.WriteLine("################");
                Console.WriteLine("1. First Name: ");
                customer.FirstName = Console.ReadLine()!;
                Console.WriteLine("2. Last Name: ");
                customer.LastName = Console.ReadLine()!;
                Console.WriteLine("3. Email: ");
                customer.Email = Console.ReadLine()!;
                Console.WriteLine("4. Password: ");
                customer.Password = Console.ReadLine()!;
                Console.WriteLine("5. Street Name: ");
                customer.StreetName = Console.ReadLine()!;
                Console.WriteLine("6. Postal Code: ");

                if (int.TryParse(Console.ReadLine(), out int postalCode) && postalCode >= 10000 && postalCode <= 99999)
                {
                    customer.PostalCode = postalCode;
                }
                else
                {
                    Console.WriteLine("Not a valid postal code... Edit your profil to correct it.(ex. 12345)");
                }
                Console.WriteLine("7. City: ");
                customer.City = Console.ReadLine()!;

                bool customerAdded = _customerService.CreateCustomer(customer);

                if(customerAdded == true)
                {
                    Console.Clear();
                    Console.WriteLine("Customer was added successfully!");
                    Console.Write("\n\nPress any key to continue...");
                    Console.ReadKey();
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Something went wrong. Please try again!");
                    Console.Write("\n\nPress any key to continue...");
                    Console.ReadKey();
                }
            }
        }
    }
}
