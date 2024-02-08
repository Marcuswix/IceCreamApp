using Share.Dtos;
using Share.Entities;
using Share.Services;

namespace PresentationIceCreamConsoleApp.Views
{
    public class EditCustomer
    {
        private readonly CustomerService _customerService;

        public EditCustomer(CustomerService customerService)
        {
            _customerService = customerService;
        }

        public async Task ShowEditCustomer()
        {
            Console.Clear();
            Console.WriteLine("<Edit Account>");
            Console.WriteLine("##############");
            Console.WriteLine("Write the email to edit an account: ");
            string customerToFind = Console.ReadLine()!;

            var result = await _customerService.UpdateCustomer(customerToFind, editedCustomer =>
            {
                Console.Clear();
                Console.WriteLine("Enter new first name: ");
                editedCustomer.FirstName = Console.ReadLine()!;

                Console.WriteLine("Enter new last name: ");
                editedCustomer.LastName = Console.ReadLine()!;

                Console.WriteLine("Enter new Email: ");
                editedCustomer.Email = Console.ReadLine()!;

                Console.WriteLine("Enter new Password: ");
                editedCustomer.Password = Console.ReadLine()!;

                if (editedCustomer.Address == null)
                {
                    editedCustomer.Address = new AddressEntity();
                }

                Console.WriteLine("Enter new Street Name: ");
                editedCustomer.Address.StreetName = Console.ReadLine()!;

                Console.WriteLine("Enter new Postal code: ");

                int.TryParse(Console.ReadLine(), out int postalCode);
                editedCustomer.Address.PostalCode = postalCode;

                Console.WriteLine("Enter new City: ");
                editedCustomer.Address.City = Console.ReadLine()!;
            });

            if (result == true)
            {
                Console.Clear();
                Console.WriteLine("The Account was successfully updated. Press any key to continue.");
                Console.ReadKey();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Something went wrong... Press any key to continue.");
                Console.ReadKey();
            }
        }
    }
}
