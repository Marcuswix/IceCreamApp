using Microsoft.Extensions.DependencyInjection;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Share.Dtos;
using Share.Services;
using System.Windows;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace IceCreamAppWPF.ViewModels
{
    public partial class UserListViewModel : ObservableObject
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly CustomerService _customerService;
        private ObservableCollection<Customer> _customers;

        public UserListViewModel(IServiceProvider serviceProvider, CustomerService customerService)
        {
            _serviceProvider = serviceProvider;
            _customerService = customerService;
            Customers = new ObservableCollection<Customer>();
        }

        [RelayCommand]
        public void NavigateToMainMenu()
        {
            var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();

            // Går till huvudmenyn
            mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<WelcomePageViewModel>();
        }

        public ObservableCollection<Customer> Customers
        {
            get => _customers;
            set => SetProperty(ref _customers, value);
        }

        [RelayCommand]
        public void NavigateToAddCustomer()
        {
            var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();

            // Går till sidan för att lägga till en ny kund
            mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<AddCustomerViewModel>();
        }

        [RelayCommand]
        public void NavigateToEditCustomer(Customer customer)
        {
            var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
            var editCustomerViewModel = _serviceProvider.GetRequiredService<EditCustomerViewModel>();

            // Ladda den aktuella kunden och skicka med den till redigeringsvyn
            editCustomerViewModel.LoadCustomer(customer);

            // Går till redigeringsvyn
            mainViewModel.CurrentViewModel = editCustomerViewModel;
        }

        [RelayCommand]
        public async Task ShowAllCustomers()
        {
            var result = await _customerService.GetAllCustomers();

            if (result != null)
            {
                Customers.Clear();
                foreach (var customer in result)
                {
                    Customers.Add(customer);
                }
            }
        }

        [RelayCommand]
        public async Task DeleteCustomer(string email)
        {
            var result = _customerService.DeleteCustomer(email);

            if (result)
            {
                await ShowAllCustomers();
            }
        }
    }
}
