using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Share.Dtos;
using Share.Services;

namespace IceCreamAppWPF.ViewModels
{
    public partial class AddCustomerViewModel : ObservableObject
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly CustomerService _customerService;
        private string _successMessage;

        public AddCustomerViewModel(IServiceProvider serviceProvider, CustomerService customerService)
        {
            _serviceProvider = serviceProvider;
            _customerService = customerService;
            Customer = new Customer();
        }

        public string SuccessMessage
        {
            get => _successMessage;
            set => SetProperty(ref _successMessage, value);
        }

        public Customer Customer { get; set; }

        [RelayCommand]
        public void NavigateToMainMenu()
        {
            var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
            mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<WelcomePageViewModel>();
        }

        [RelayCommand]
        public void AddCustomerToList()
        {
            var result = _customerService.CreateCustomer(Customer);

            if (result)
            {
                SuccessMessage = "You have been successfully added!";

                Customer.FirstName = "";
                Customer.LastName = "";
                Customer.Email = "";
                Customer.Password = "";
                Customer.StreetName = "";
                Customer.City = "";
            }
        }
    }
}
