using Xunit;
using Share.Entities;
using Share.Repositories;
using Share.Contexts;
using Microsoft.EntityFrameworkCore;
using Share.Services;
using Share.Dtos;

namespace IceCreamTests.IntegrationTests
{
    public class ProductServiceTest
    {

        private readonly DataContext _dataContext = new DataContext(new DbContextOptionsBuilder<DataContext>()
        .UseInMemoryDatabase($"{Guid.NewGuid()}").Options);

        //Create
        [Fact]
        public void CreateProduct_ShouldCreateAProduct_ThenReturnTrue()
        {
            //Arrange
            var productRepo = new ProductRepository (_dataContext);
            var productService = new ProductsService (productRepo, _dataContext);
            var product = new Product { Title = "Test", ArticleNumber = "A1", Description = "Test ett test", SpecificationAsJson = "Test", Price = 12, CategoryName = "Test", ManufacturerName = "Test" };

            //Act
            var result = productService.CreateProduct(product); 
            
            //Assert
            Assert.True(result);
        }

        //Delete
        [Fact]
        public async Task DeleteProduct_ShouldDeleteAProduct_ThenReturnTrue()
        {
            //Arrang
            var productRepo = new ProductRepository(_dataContext);
            var productService = new ProductsService(productRepo, _dataContext);
            var product = new Product { Title = "Test", ArticleNumber = "A1", Description = "Test ett test", SpecificationAsJson = "Test", Price = 12, CategoryName = "Test", ManufacturerName = "Test", SubcategoryName = "testtest", ImageUrl = "test.se"};
            productService.CreateProduct(product);

            //Act
            var result = await productService.DeleteProduct("A1");

            //Assert
            Assert.True(result);
        }

        //Update
        [Fact]
        public async Task UpdateProduct_ShouldUpdateAProduct_ThenReturnTrue()
        {
            //Arrange
            var productRepo = new ProductRepository(_dataContext);
            var productEntity = new ProductEntity();
            var productService = new ProductsService(productRepo, _dataContext);
            var product = new Product { Title = "Test", ArticleNumber = "A1", Description = "Test ett test", SpecificationAsJson = "Test", Price = 12, CategoryName = "Test", ManufacturerName = "Test", SubcategoryName = "testtest", ImageUrl = "test.se" };
            productService.CreateProduct(product);

            //Act
            var result = await productService.UpdateProduct("A1", existingProduct =>
            {
                existingProduct.Title = "Update";
            });

            //Assert
            Assert.True(result);
            var updatedProduct = await productRepo.GetOne(x => x.ArticleNumber == "A1");
            Assert.Equal("Update", updatedProduct.Title);
        }

        //Read
        [Fact]
        public void GetOneProduct_ShouldGetAProduct_ThenReturnThatDTO()
        {
            //Arrange
            var productRepo = new ProductRepository(_dataContext);
            var productService = new ProductsService(productRepo, _dataContext);
            var product = new Product { Title = "Test", ArticleNumber = "A1", Description = "Test ett test", SpecificationAsJson = "Test", Price = 12, CategoryName = "Test", ManufacturerName = "Test", SubcategoryName = "testtest", ImageUrl = "test.se" };
            productService.CreateProduct(product);

            //Act
            var result = productService.GetOneProduct(product.Title);

            //Assert
            Assert.NotNull(result);
            Assert.Equal("Test", product.Title);
        }

        [Fact]
        public async Task GetAllProducts_ShouldAllProducts_ThenReturnThatList()
        {
            //Arrange
            var productRepo = new ProductRepository(_dataContext);
            var productService = new ProductsService(productRepo, _dataContext);
            var product = new Product { Title = "Test", ArticleNumber = "A1", Description = "Test ett test", SpecificationAsJson = "Test", Price = 12, CategoryName = "Test", ManufacturerName = "Test", SubcategoryName = "testtest", ImageUrl = "test.se" };
            productService.CreateProduct(product);

            //Act
            var result = await productService.GetAllProducts();

            //Assert
            Assert.NotNull(result);
            Assert.Contains(result, x => x.Title == "Test");
        }
    }
}

