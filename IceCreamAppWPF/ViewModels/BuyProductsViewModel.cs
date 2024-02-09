using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IceCreamAppWPF.Views;
using Microsoft.Extensions.DependencyInjection;
using Share.Dtos;
using Share.Services;
using System.Collections.ObjectModel;

namespace IceCreamAppWPF.ViewModels
{
    public partial class BuyProductsViewModel : ObservableObject
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ProductsService _productsService;
        private ObservableCollection<Product> _products = new ObservableCollection<Product>();
        private readonly OrderService _orderService;

        public BuyProductsViewModel(IServiceProvider serviceProvider, ProductsService productsService, OrderService orderService)
        {
            _serviceProvider = serviceProvider;
            _productsService = productsService;
            _orderService = orderService;

            LoadProducts();
        }

        private async Task LoadProducts()
        {
            await GetAllProducts();
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

        public async Task GetAllProducts()
        {
            var result = await _productsService.GetAllProducts();

            if (result != null)
            {
                //Products.Clear();
                foreach (var product in result)
                {
                    Products.Add(product);
                }
            }
        }

        [RelayCommand]
        public async Task OrderProduct()
        {
            //var result = await _orderService.AddOrder(email, articleNumber, quantity );

            //if (result == true)
            //{
            //    SuccessMessage = "Product has successfully been added to Cart";
            //}
        }

        private string _successMessage;

        public string SuccessMessage
        {
            get => _successMessage;
            set => SetProperty(ref _successMessage, value);
        }
    }
}
