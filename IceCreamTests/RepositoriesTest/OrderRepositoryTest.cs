using Microsoft.EntityFrameworkCore;
using Share.Contexts;
using Share.Entities;
using Share.Repositories;

namespace IceCreamTests.RepositoriesTest
{
    public class OrderRepositoryTest
    {
        private readonly DataContextOrders _dataContextOrders = new DataContextOrders(new DbContextOptionsBuilder<DataContextOrders>()
        .UseInMemoryDatabase($"{Guid.NewGuid()}").Options);

        //Create
        [Fact]
        public async Task AddOrder_ShouldAddAOrderToDatabase_ThenReturnTrue()
        {
            //Arrange
            var orderRepo = new OrderRepository(_dataContextOrders);
            var order = new Order { Id = 100, CustomerId = 100, OrderDetailsId = 100, TotalPrice = 1, OrderDate = DateTime.Now, OrderDetails = new OrderDetail { Id = 100, ProductId = "A1", Quantity = 1, UnitPrice = 12 } };

            //Act
            var result = await orderRepo.AddOrder(order);

            //Assert
            Assert.True(result);
        }


        //Read
        [Fact]
        public async Task GetAOrder_ShouldGetAOrder_ThenReturnTharOrder()
        {
            //Arrange
            var orderRepo = new OrderRepository(_dataContextOrders);
            var order = new Order { Id = 100, CustomerId = 100, OrderDetailsId = 100, TotalPrice = 1, OrderDate = DateTime.Now, OrderDetails = new OrderDetail { Id = 100, ProductId = "A1", Quantity = 1, UnitPrice = 12 } };
            orderRepo.AddOrder(order);
            int Id = 100;

            //Act
            var result = await orderRepo.GetAOrder(Id);

            //Assert
            Assert.Equal(100, result.Id);
            Assert.Equal(order, result);
        }

        [Fact]
        public async Task GetAllOrders_ShouldGetAllOrdersFromDatabase_ThenReturnAListWithAllOrders()
        {
            //Arrange
            var orderRepo = new OrderRepository(_dataContextOrders);
            var order = new Order { Id = 101, CustomerId = 100, OrderDetailsId = 100, TotalPrice = 2, OrderDate = DateTime.Now, OrderDetails = new OrderDetail { Id = 100, ProductId = "A1", Quantity = 1, UnitPrice = 12 } };
            orderRepo.AddOrder(order);
            
            //Act
            var result = await orderRepo.GetAllOrders();

            //Assert
            Assert.Contains(order, result);
        }

        //Update
        [Fact]
        public async Task EditOrder_ShouldOverwriteAExistingOrder_ThenReturnTrue()
        {
            //Arrange
            var orderRepo = new OrderRepository(_dataContextOrders);
            var oldOrder = new Order { Id = 100, CustomerId = 100, OrderDetailsId = 100, TotalPrice = 1, OrderDate = DateTime.Now, OrderDetails = new OrderDetail { Id = 100, ProductId = "A1", Quantity = 1, UnitPrice = 12 } };
            await orderRepo.AddOrder(oldOrder);
            var updatedOrder = new Order { Id = 101, CustomerId = 100, OrderDetailsId = 100, TotalPrice = 2, OrderDate = DateTime.Now, OrderDetails = new OrderDetail { Id = 100, ProductId = "A1", Quantity = 1, UnitPrice = 12 } };

            //Act
            var result = await orderRepo.EditOrder(updatedOrder);

            //Assert
            Assert.True(result);
        }

        //Delete
        [Fact]
        public async Task DeleteOrder_ShouldDeleteAOrderToDatabase_ThenReturnTrue()
        {
            //Arrange
            var orderRepo = new OrderRepository(_dataContextOrders);
            var order = new Order { Id = 100, CustomerId = 100, OrderDetailsId = 100, TotalPrice = 1, OrderDate = DateTime.Now, OrderDetails = new OrderDetail { Id = 100, ProductId = "A1", Quantity = 1, UnitPrice = 12 } };
            orderRepo.AddOrder(order);

            //Act
            var result = await orderRepo.DeleteOrder(order.Id);

            //Arrange
            Assert.True(result);
        }
    }
}
