using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Share.Services;

namespace IceCreamAppWPF.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly CustomerService _customerService;

        public LoginViewModel(IServiceProvider serviceProvider, CustomerService customerService)
        {
            _serviceProvider = serviceProvider;
            _customerService = customerService;
        }

        [RelayCommand]
        public void NavigateToMainMenu()
        {
            var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
            mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<WelcomePageViewModel>();
        }

        private string _email;
        private string _password;

        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public void NavigateBuyProducts()
        {
            var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
            mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<BuyProductsViewModel>();
        }

        [RelayCommand]
        public async Task Login()
        {
            var result = await _customerService.GetACustomer(Email);

            if(result.Password == Password)
            {
                NavigateBuyProducts();
            }
        }
    }
}
