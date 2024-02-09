using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Share.Dtos;
using Share.Services;
using System.Collections.ObjectModel;

namespace IceCreamAppWPF.ViewModels
{

    public partial class ProductListViewModel : ObservableObject
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ProductsService _productsService;
        private ObservableCollection<Product> _products;

        public ProductListViewModel(IServiceProvider serviceProvider, ProductsService productsService)
        {
            _serviceProvider = serviceProvider;
            _productsService = productsService;
            Products = new ObservableCollection<Product>();
        }

        [RelayCommand]
        public void NavigateToMainMenu()
        {
            var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();

            //Går till
            mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<WelcomePageViewModel>();
        }

        public ObservableCollection<Product> Products
        {
            get => _products;
            set => SetProperty(ref _products, value);
        }

        [RelayCommand]
        public void NavigateToAddProduct()
        {
            var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
            mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<AddProductViewModel>();
        }

        [RelayCommand]
        public async Task ShowAllProducts()
        {
            var result = await _productsService.GetAllProducts();

            if (result != null)
            {
                Products.Clear();
                foreach (var product in result)
                {
                    Products.Add(product);
                }
            }
        }

        [RelayCommand]
        public async Task DeleteProduct(string articleNumber)
        {
            var result = await _productsService.DeleteProduct(articleNumber);

            if (result == true)
            {
                await ShowAllProducts();
            }
        }


        [RelayCommand]
        public void NavigateToEditProduct(Product product)
        {
            var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
            var editProductViewModel = _serviceProvider.GetRequiredService<EditProductViewModel>();
           
            editProductViewModel.LoadProduct(product);

            mainViewModel.CurrentViewModel = editProductViewModel;
        }
    }
}
