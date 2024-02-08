using Microsoft.EntityFrameworkCore;
using Share.Contexts;
using Share.Repositories;
using Share.Services;
using Share.Entities;

namespace IceCreamTests.ServicesTest
{
    public class OrderServiceTests
    {
        private readonly DataContextOrders _dataContextOrders = new DataContextOrders(new DbContextOptionsBuilder<DataContextOrders>()
        .UseInMemoryDatabase($"{Guid.NewGuid()}").Options);

        //Create
        [Fact]
        public async Task AddOrder_ShouldAddAOrder_ThenReturnTrue()
        {
            //Arrange
            var orderRepo = new OrderRepository(_dataContextOrders);
            var orderService = new OrderService(orderRepo);
            var product = new ProductEntity { Title = "Updated", ArticleNumber = "A1", Description = "Test ett test", SpecificationAsJson = "Test", Price = 12, CategoryId = 1, Category = new CategoryEntity { CategoryId = 1, CategoryName = "Test" }, ManufacturerId = 2, Manufacturer = new ManufacturerEntity { ManufacturerId = 2, ManufacturerName = "Test" } };
            var customer = new CustomerEntity { Id = 1, FirstName = "Test", LastName = "Test", Email = "test@test.se", Password = "test", AddressId = 0 };

            //Act
            var result = await orderService.AddOrder(product, customer, 1);

            //Assert
            Assert.True(result);
        }

        //Read
        [Fact]
        public async Task GetAllOrders_ShouldGetAllOrders_ThenReturnAListWithAllOrders()
        {
            //Arrange
            var orderRepo = new OrderRepository(_dataContextOrders);
            var orderService = new OrderService(orderRepo);
            var product = new ProductEntity { Title = "Updated", ArticleNumber = "A1", Description = "Test ett test", SpecificationAsJson = "Test", Price = 12, CategoryId = 1, Category = new CategoryEntity { CategoryId = 1, CategoryName = "Test" }, ManufacturerId = 2, Manufacturer = new ManufacturerEntity { ManufacturerId = 2, ManufacturerName = "Test" } };
            var customer = new CustomerEntity { Id = 1, FirstName = "Test", LastName = "Test", Email = "test@test.se", Password = "test", AddressId = 0 };
            await orderService.AddOrder(product, customer, 1);

            //Act
            var result = await orderService.GetAllOrders();

            var numbers = result.Count();

            //Assert
            Assert.NotNull(result);
            Assert.Equal(1, numbers);
        }

        [Fact]
        public async Task GetAnOrder_ShouldGetAnOrderById_ThenReturnThatOrder()
        {
            //Arrange
            var orderRepo = new OrderRepository(_dataContextOrders);
            var orderService = new OrderService(orderRepo);
            var product = new ProductEntity { Title = "Updated", ArticleNumber = "A1", Description = "Test ett test", SpecificationAsJson = "Test", Price = 12, CategoryId = 1, Category = new CategoryEntity { CategoryId = 1, CategoryName = "Test" }, ManufacturerId = 2, Manufacturer = new ManufacturerEntity { ManufacturerId = 2, ManufacturerName = "Test" } };
            var customer = new CustomerEntity { Id = 1, FirstName = "Test", LastName = "Test", Email = "test@test.se", Password = "test", AddressId = 0 };
            await orderService.AddOrder(product, customer, 1);

            //Act
            var result = await orderService.GetAnOrder(1);

            //Assert
            Assert.NotNull(result);
            Assert.Equal("A1", result.OrderDetails.ProductId);
            Assert.Equal(1, result.CustomerId);
        }

        //Update
        [Fact]
        public async Task UpdateOrder_ShouldUpdateAOrder_ThenReturnTrue()
        {
            //Arrange
            var orderRepo = new OrderRepository(_dataContextOrders);
            var orderService = new OrderService(orderRepo);
            var order = new Order { Id = 100, CustomerId = 100, OrderDetailsId = 100, TotalPrice = 1, OrderDate = DateTime.Now, OrderDetails = new OrderDetail { Id = 100, ProductId = "A1", Quantity = 1, UnitPrice = 12 } };
            await orderRepo.AddOrder(order);
            var updatedOrder = new Order { Id = 100, CustomerId = 101, OrderDetailsId = 101, TotalPrice = 2, OrderDate = DateTime.Now, OrderDetails = new OrderDetail { Id = 100, ProductId = "A1", Quantity = 1, UnitPrice = 12 } };

            //Act
            var result = await orderService.UpdateOrder(order, existingOrder =>
            {
                existingOrder.CustomerId = updatedOrder.CustomerId;
                existingOrder.TotalPrice = updatedOrder.TotalPrice;
            });

            //Assert
            Assert.True(result);
            var existingOrder = await orderRepo.GetAOrder(100);
            Assert.Equal(updatedOrder.Id, existingOrder.Id);
            Assert.Equal(updatedOrder.TotalPrice, existingOrder.TotalPrice);
        }

        //Delete
        [Fact]
        public async Task DeleteOrder_ShouldDeleteAOrder_ThenReturnTrue()
        {
            //Arrange
            var orderRepo = new OrderRepository(_dataContextOrders);
            var orderService = new OrderService(orderRepo);
            var order = new Order { Id = 100, CustomerId = 100, OrderDetailsId = 100, TotalPrice = 1, OrderDate = DateTime.Now, OrderDetails = new OrderDetail { Id = 100, ProductId = "A1", Quantity = 1, UnitPrice = 12 } };
            await orderRepo.AddOrder(order);

            //Act
            var result = await orderService.DeleteOrder(100);

            //Assert
            Assert.True(result);
        }
    }
}
