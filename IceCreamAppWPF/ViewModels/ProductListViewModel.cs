using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;

namespace IceCreamAppWPF.ViewModels
{

    public partial class ProductListViewModel : ObservableObject
    {
        private readonly IServiceProvider _serviceProvider;

        public ProductListViewModel(IServiceProvider serviceProvider)
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
