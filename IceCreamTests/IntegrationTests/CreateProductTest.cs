using Share.Dtos;
using Share.Entities;
using Share.Repositories;
using Share.Services;
using Moq;
using Share.Contexts;

namespace IceCreamTests.IntegrationTests
{
    public class CreateProductTest
    {

        private readonly ProductsService _productsService;
        private readonly DataContext _dataContext;

        public CreateProductTest(ProductsService productsService, DataContext dataContext)
        {
            _productsService = productsService;
            _dataContext = dataContext;
        }


        [Fact]
        public void CreateProduct_ShouldCreateAProducts_ThenReturnTrue()
        {
            // Arrange
            var productRepositoryMock = new Mock<ProductRepository>();
            var categoryRepositoryMock = new Mock<CategoryRepository>();
            var dataContaxtMock = new Mock<DataContext>();
            

            var productService = new ProductsService(productRepositoryMock.Object, categoryRepositoryMock.Object, dataContaxtMock.Object);

            Product product = new Product { Title = "Test", CategoryName = "Category Test", ManufacturerName = "Manufacturer Test", ArticleNumber = "A1", Description = "Test", Price = 12, SpecificationAsJson = "Test As Json" };

            //Act
            var result = productService.CreateProduct(product);

            //Assert
            Assert.True(result);
        }
    }
}
