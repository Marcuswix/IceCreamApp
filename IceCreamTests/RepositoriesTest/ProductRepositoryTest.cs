using Microsoft.EntityFrameworkCore;
using Share.Contexts;
using Share.Entities;
using Share.Repositories;

namespace IceCreamTests.RepositoriesTest
{
    public class ProductRepositoryTest
    {

         private readonly DataContext _dataContext = new DataContext(new DbContextOptionsBuilder<DataContext>()
        .UseInMemoryDatabase($"{Guid.NewGuid()}").Options);

        //Create
        [Fact]
        public async Task Create_ShouldCreateAProductEntity_ThenReturnThatProductEntity()
        {
            //Arrange
            var productRepo = new ProductRepository(_dataContext);
            var productEntity = new ProductEntity { Title = "Test", ArticleNumber = "A1", Description = "Test ett test", SpecificationAsJson = "Test", Price = 12, CategoryId = 1, Category = new CategoryEntity { CategoryId = 1, CategoryName = "Test" }, ManufacturerId = 2, Manufacturer = new ManufacturerEntity { ManufacturerId = 2, ManufacturerName = "Test" } };

            //Act
            var result = await productRepo.Create(productEntity);

            //Assert
            Assert.NotNull(result);
        }

        //Read
        [Fact]
        public async Task GetAll_ShouldGetAllProductsFromDatabaseTabels_ThenReturnAllProductsInAList()
        {
            //Arrange
            var repoProducts = new ProductRepository(_dataContext);
            var product = new ProductEntity { Title = "Test", ArticleNumber = "A1", Description = "Test ett test", SpecificationAsJson = "Test", Price = 12, CategoryId = 1, Category = new CategoryEntity { CategoryId = 1, CategoryName = "Test" }, ManufacturerId = 2, Manufacturer = new ManufacturerEntity { ManufacturerId = 2, ManufacturerName = "Test" } };
            var resultCreate = repoProducts.Create(product);

            //Act
            var result = await repoProducts.GetAllProducts();

            var number = result.Count();

            //Assert
            Assert.NotNull(result);
            Assert.Equal(1, number);
            var check = result.FirstOrDefault(x => x.Title == "Test");
            Assert.Equal("Test", check.Title);
        }

        [Fact]
        public async Task GetOneProduct_ShouldGetAProductFromDatabaseTabels_ThenReturnThatProduct()
        {
            //Arrage
            var prodRepo = new ProductRepository(_dataContext);
            var product = new ProductEntity { Title = "Test", ArticleNumber = "A1", Description = "Test ett test", SpecificationAsJson = "Test", Price = 12, CategoryId = 1, Category = new CategoryEntity { CategoryId = 1, CategoryName = "Test" }, ManufacturerId = 2, Manufacturer = new ManufacturerEntity { ManufacturerId = 2, ManufacturerName = "Test" } };
            var resultCreate = await prodRepo.Create(product);

            //Act
            var result = await prodRepo.GetOne(x => x.ArticleNumber == "A1");

            //Assert
            Assert.NotNull(result);
            Assert.Equal("Test", result.Title);
        }

        //Update
        [Fact]
        public void UpdateProduct_ShouldUpdateAProductEntity_ThenReturnThatProductEntity()
        {
            //Arrange
            var productRepo = new ProductRepository(_dataContext);
            var productEntity = new ProductEntity { Title = "Test", ArticleNumber = "A1", Description = "Test ett test", SpecificationAsJson = "Test", Price = 12, CategoryId = 1, Category = new CategoryEntity { CategoryId = 1, CategoryName = "Test" }, ManufacturerId = 2, Manufacturer = new ManufacturerEntity { ManufacturerId = 2, ManufacturerName = "Test" } };
            productRepo.Create(productEntity).ConfigureAwait(false);
            var productEntityUpdated = new ProductEntity { Title = "Updated", ArticleNumber = "A1", Description = "Test ett test", SpecificationAsJson = "Test", Price = 12, CategoryId = 1, Category = new CategoryEntity { CategoryId = 1, CategoryName = "Test" }, ManufacturerId = 2, Manufacturer = new ManufacturerEntity { ManufacturerId = 2, ManufacturerName = "Test" } };

            //Act
            var result = productRepo.UpdateProduct(productEntityUpdated);

            //Assert
            Assert.NotNull(result);
            Assert.Equal("Updated", result.Title);
        }

        //Delete
        [Fact]
        public async Task ProductRepositoriy_DeleteProduct_ShouldDeleteAProduct_ThenReturnTrue()
        {
            //Arrange
            var productRepository = new ProductRepository(_dataContext);
            var productEntity = new ProductEntity { Title = "Test", ArticleNumber = "A1", Description = "Test ett test", SpecificationAsJson = "Test", Price = 12, CategoryId = 1, Category = new CategoryEntity { CategoryId = 1, CategoryName = "Test" }, ManufacturerId = 2, Manufacturer = new ManufacturerEntity { ManufacturerId = 2, ManufacturerName = "Test" } };
            await productRepository.Create(productEntity);

            //Act
            var result = productRepository.DeleteProduct(productEntity);

            //Assert
            Assert.True(result);
        }
    }
}
