using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IceCreamAppWPF.Views;
using Microsoft.Extensions.DependencyInjection;

namespace IceCreamAppWPF.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableObject _currentViewModel;

        private readonly IServiceProvider _serviceProvider;

        public MainViewModel(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            CurrentViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        }

        [RelayCommand]
        public void NavigateToUserList()
        {
            var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
            mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<UserListViewModel>();
        }

        [RelayCommand]
        public void NavigateToBuyProducts()
        {
            var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
            mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<BuyProductsViewModel>();
        }

        [RelayCommand]
        public void NavigateToProductList() 
        {
            var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
            mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<ProductListViewModel>();
            
        }

    }
}
