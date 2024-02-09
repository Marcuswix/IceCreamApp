using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Share.Dtos;
using Share.Services;
namespace IceCreamAppWPF.ViewModels
{
    public partial class AddProductViewModel : ObservableObject
    {

        private readonly IServiceProvider _serviceProvider;
        private readonly ProductsService _productsService;
        private string _successMessage;

        public AddProductViewModel(IServiceProvider serviceProvider, ProductsService productsService)
        {
            _serviceProvider = serviceProvider;
            _productsService = productsService;
            Product = new Product();
        }

        public Product Product { get; set; }

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
        public void AddProduct()
        {
            var result = _productsService.CreateProduct(Product);

            if (result == true)
            {
                SuccessMessage = "Product has successfully been added!";

                Product.Title = "";
                Product.Description = "";
                Product.ManufacturerName = "";
                Product.ArticleNumber = "";
                Product.SubcategoryName = "";
                Product.CategoryName = "";
                Product.ImageUrl = "";
            }
            else
            {
                SuccessMessage = "Something went wrong...";
            }
        }
    }
}
