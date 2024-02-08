using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IceCreamAppWPF.Views;
using Microsoft.Extensions.DependencyInjection;
namespace IceCreamAppWPF.ViewModels
{
    public partial class BuyProductsViewModel : ObservableObject
    {
        private readonly IServiceProvider _serviceProvider;

        public BuyProductsViewModel(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        [RelayCommand]
        public void NavigateToMainMenu()
        {
            var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();

            //Går till
            mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        }
    }
}
