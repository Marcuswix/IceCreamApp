using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Share.Dtos;
using Share.Services;
using System.Threading.Tasks;

namespace IceCreamAppWPF.ViewModels
{
    public partial class EditCustomerViewModel : ObservableObject
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly CustomerService _customerService;

        public EditCustomerViewModel(IServiceProvider serviceProvider, CustomerService customerService)
        {
            _serviceProvider = serviceProvider;
            _customerService = customerService;


            SuccessMessage = "";
        }

        private Customer _customer;

        public Customer Customer
        {
            get => _customer;
            set => SetProperty(ref _customer, value);
        }

        public void LoadCustomer(Customer customer)
        {
            Customer = customer;
            SuccessMessage = "";
        }

        [RelayCommand]
        public void NavigateToMainMenu()
        {
            var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
            mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<WelcomePageViewModel>();
        }

        public void NavigateToCustomerMenu()
        {
            var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
            mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<UserListViewModel>();
        }

        [RelayCommand]
        public async Task EditCustomer()
        {
            if (Customer != null)
            {
                var result = await _customerService.UpdateCustomer(Customer.Email, customerEntity =>
                {
                    customerEntity.FirstName = Customer.FirstName;
                    customerEntity.LastName = Customer.LastName;
                    customerEntity.Email = Customer.Email;
                    customerEntity.Password = Customer.Password;
                    customerEntity.Address.StreetName = Customer.StreetName;
                    customerEntity.Address.PostalCode = Customer.PostalCode;
                    customerEntity.Address.City = Customer.City;
                });

                if (result)
                {
                    SuccessMessage = "Customer updated successfully!";

                    await Task.Delay(1000);

                    NavigateToCustomerMenu();
                }
                else
                {
                    SuccessMessage = "Failed to update customer.";
                }
            }
            else
            {
                SuccessMessage = "Customer data is empty.";
            }
        }

        private string _successMessage;

        public string SuccessMessage
        {
            get => _successMessage;
            set => SetProperty(ref _successMessage, value);
        }
    }
}
