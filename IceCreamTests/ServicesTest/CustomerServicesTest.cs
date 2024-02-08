using Microsoft.EntityFrameworkCore;
using Moq;
using Share.Contexts;
using Share.Dtos;
using Share.Repositories;
using Share.Services;

namespace IceCreamTests.IntegrationTests
{
    public class CustomerServicesTest
    {

        private readonly DataContext _dataContext = new DataContext(new DbContextOptionsBuilder<DataContext>()
        .UseInMemoryDatabase($"{Guid.NewGuid()}").Options);

        //Create
        [Fact]
        public void CustomerService_CreateCustomer_ShouldCreateCustomer_ThenReturnTrue()
        {
            //Arrange
            var customerRepo = new CustomerRepository(_dataContext);
            var addressRepo = new AddressRepository(_dataContext);
            var customerService = new CustomerService(customerRepo, addressRepo, _dataContext);
            var customer = new Customer { Id = 1, FirstName = "Test", LastName = "Test", Email = "test@test.se", Password = "test", StreetName = "testgatan", City = "Teststan", PostalCode = 12345 };

            //Act
            var result = customerService.CreateCustomer(customer);

            //Assert
            Assert.True(result);
        }

        //Read
        [Fact]
        public async Task CustomerService_GetAllCustomers_ShouldGetALlCustomers_ThenReturnAListWithAllCustomers()
        {
            //Arrange
            var customerRepo = new CustomerRepository(_dataContext);
            var addressRepo = new AddressRepository(_dataContext);
            var customerService = new CustomerService(customerRepo, addressRepo, _dataContext);
            var customer = new Customer { Id = 1, FirstName = "Marcus", LastName = "Test", Email = "test@test.se", Password = "test", StreetName = "testgatan", City = "Teststan", PostalCode = 12345 };
            customerService.CreateCustomer(customer);

            //Act
            var list = await customerService.GetAllCustomers();

            //Assert
            Assert.Contains(list, c => c.FirstName == "Marcus");
            Assert.NotNull(list);
        }

        [Fact]
        public async Task GetACustomer_ShouldGetACustomer_ThenReturnThatCustomer()
        {
            //Arrange
            var customerRepo = new CustomerRepository(_dataContext);
            var addressRepo = new AddressRepository(_dataContext);
            var customerService = new CustomerService(customerRepo, addressRepo, _dataContext);
            var customer = new Customer { Id = 1, FirstName = "Marcus", LastName = "Test", Email = "test@test.se", Password = "test", StreetName = "testgatan", City = "Teststan", PostalCode = 12345 };
            customerService.CreateCustomer(customer);

            //Act
            var result = await customerService.GetACustomer(customer.Email);

            //Assert
            Assert.Equal(1, result.Id);
            Assert.Equal("Marcus", result.FirstName);
        }

        //Update
        [Fact]
        public async Task UpdateCustomer_ShouldUpdateACustomer_ThenReturnTrue()
        {
            //Arrange
            var customerRepo = new CustomerRepository(_dataContext);
            var AddressRepo = new AddressRepository(_dataContext);
            var customerService = new CustomerService(customerRepo, AddressRepo, _dataContext);
            var customer = new Customer { Id = 1, FirstName = "Marcus", LastName = "Test", Email = "test@test.se", Password = "test", StreetName = "testgatan", City = "Teststan", PostalCode = 12345 };
            customerService.CreateCustomer(customer);
            var updatedCustomer = new Customer { Id = 1, FirstName = "Hanna", LastName = "Testson", Email = "hanna@test.se", Password = "test", StreetName = "testgatan", City = "Teststan", PostalCode = 12345 };

            //Act
            var result = await customerService.UpdateCustomer(customer.Email, existingCustomer =>
            {
                existingCustomer.FirstName = updatedCustomer.FirstName;
                existingCustomer.LastName = updatedCustomer.LastName;
                existingCustomer.Email = updatedCustomer.Email;
            });

            //Assert
            Assert.True(result);
            var customerUpdatedTest = await customerService.GetACustomer(updatedCustomer.Email);
            Assert.Equal("Hanna", customerUpdatedTest.FirstName);
            Assert.Equal("Testson", customerUpdatedTest.LastName);
        }

        //Delete
        [Fact]
        public void DeleteCustomer_FromList_ShouldDeleteACustomer_ThenReturnTrue()
        {
            //Arrange
            var customerRepo = new CustomerRepository(_dataContext);
            var addressRepo = new AddressRepository(_dataContext);
            var customerService = new CustomerService(customerRepo, addressRepo, _dataContext);
            var customer = new Customer { Id = 1, FirstName = "Marcus", LastName = "Test", Email = "test@test.se", Password = "test", StreetName = "testgatan", City = "Teststan", PostalCode = 12345 };
            customerService.CreateCustomer(customer);

            //Act
            var result = customerService.DeleteCustomer("test@test.se");

            //Assert
            Assert.True(result);
        }
    }
}
