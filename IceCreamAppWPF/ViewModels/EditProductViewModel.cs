using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Share.Contexts;
using Share.Dtos;
using Share.Entities;
using Share.Services;

namespace IceCreamAppWPF.ViewModels
{
    public partial class EditProductViewModel : ObservableObject
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ProductsService _productsService;


        public EditProductViewModel(IServiceProvider serviceProvider, ProductsService productsService)
        {
            _serviceProvider = serviceProvider;
            _productsService = productsService;
            SuccessMessage = "";
        }

        private Product _product;

        public Product Product
        {
            get => _product;
            set => SetProperty(ref _product, value);
        }

        public void LoadProduct(Product product)
        {
            Product = product;
            SuccessMessage = "";
        }

        [RelayCommand]
        public void NavigateToMainMenu()
        {
            var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
            mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<WelcomePageViewModel>();
        }

        public void NavigateToProductMenu()
        {
            var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
            mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<ProductListViewModel>();
        }


        [RelayCommand]
        public async Task EditProduct()
        {
            if (Product != null)
            {
                var result = await _productsService.UpdateProduct(Product.ArticleNumber, productEntity =>
                {
                    productEntity.ArticleNumber = Product.ArticleNumber;
                    productEntity.Title = Product.Title;
                    productEntity.Description = Product.Description;
                    productEntity.Price = Product.Price;
                    productEntity.Manufacturer.ManufacturerName = Product.ManufacturerName;
                    productEntity.Category.CategoryName = Product.CategoryName;
                    productEntity.Category.SubCategory.SubcategoryName = Product.SubcategoryName!;

                });

                if (result)
                {
                    SuccessMessage = "Product was updated successfully!";

                    Product.ArticleNumber = "";
                    Product.Title = "";
                    Product.CategoryName = "";
                    Product.SubcategoryName = "";
                    Product.ManufacturerName = "";
                    Product.Description = "";

                    await Task.Delay(1000);
                    
                    NavigateToProductMenu();


                }
                else
                {
                    SuccessMessage = "Failed to update product.";
                }
            }
            else
            {
                SuccessMessage = "Product data is empty.";
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
