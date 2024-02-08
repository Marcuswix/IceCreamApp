using Microsoft.EntityFrameworkCore;
using Share.Contexts;
using Share.Entities;
using Share.Repositories;
using Share.Services;

namespace IceCreamTests.RepositoriesTest
{
    public class CustomerRepositoryTest
    {

        private readonly DataContext _dataContext = new DataContext(new DbContextOptionsBuilder<DataContext>()
        .UseInMemoryDatabase($"{Guid.NewGuid()}").Options);

        //Create
        [Fact]
        public async Task Base_Repository_Create_ShouldCreateACustomer_ThenReturnTrue()
        {
            //Arrange
            var customerRepository = new CustomerRepository(_dataContext);
            var customerEntity = new CustomerEntity { Id = 1, FirstName = "Test", LastName = "Test", Email = "test@test.se", Password = "test", AddressId = 0 };

            //Act
            var result = await customerRepository.Create(customerEntity);

            //Assert
            Assert.NotNull(result);
            Assert.Equal("Test", result.FirstName);
        }

        //Read
        [Fact]
        public async Task GetAllCustomers_ShouldGetALlCustomers_ThenReturnAListWithAllCustomers()
        {

            //Arrange
            var customerRepository = new CustomerRepository(_dataContext);
            var customerEntity = new CustomerEntity { Id = 0, FirstName = "Marcus", LastName = "Test", Email = "test@test.se", Password = "test", AddressId = 1 };
            await customerRepository.Create(customerEntity);

            //Act
            var list = await customerRepository.GetAll();

            //Assert
            Assert.Contains(list, c => c.FirstName == "Marcus");
            Assert.NotNull(list);
        }

        [Fact]
        public async Task GetACustomer_ShouldGetALlCustomers_ThenReturnAListWithAllCustomers()
        {
            //Arrnage
            var repoCustomer = new CustomerRepository(_dataContext);
            var repoAddress = new AddressRepository(_dataContext);
            var customerService = new CustomerService(repoCustomer, repoAddress, _dataContext);
            var customer = new CustomerEntity { Id = 1, FirstName = "Marcus", LastName = "Test", Email = "test@test.se", Password = "test", AddressId = 0 };
            await repoCustomer.Create(customer);

            //Act
            var result = await customerService.GetACustomer("test@test.se");

            //Assert
            Assert.Equal("Marcus", result.FirstName);
            Assert.Equal(customer.Id, result.Id);
        }

        [Fact]
        public async Task Exists_ShouldCheckIfACustomerAlreadyExists_IfYes_ThenReturnTrue()
        {
            //Arrange
            var customerRepo = new CustomerRepository(_dataContext);
            var customer = new CustomerEntity { Id = 1, FirstName = "Marcus", LastName = "Test", Email = "test@test.se", Password = "test", AddressId = 0 };
            await customerRepo.Create(customer);

            //Act
            var result = customerRepo.Exists(customer);

            //Assert
            Assert.True(result);
        }

        //Update
        [Fact]
        public async Task UpdateCustomer_ShouldUpdateACustomer_ThenReturnThatUpdatedCustomer()
        {
            //Arrange
            var customerRepo = new CustomerRepository(_dataContext);
            var customer = new CustomerEntity { Id = 1, FirstName = "Marcus", LastName = "Test", Email = "test@test.se", Password = "test", AddressId = 0 };
            await customerRepo.Create(customer);
            var updatedCustomer = new CustomerEntity { Id = 1, FirstName = "Olle", LastName = "Test", Email = "Olle@test.se", Password = "test", AddressId = 0 };

            //Act
            var result = customerRepo.UpdateCustomer(updatedCustomer);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Test", result.LastName);
            Assert.Equal("Olle", result.FirstName);
            Assert.NotEqual("Marcus", result.FirstName);
            Assert.NotEqual("test@test", result.Email);
        }

        //Delete
        [Fact]
        public async Task DeleteCustomer_FromList_ShouldDeleteACustomer_ThenReturnTrue()
        {
            //Arrange
            var customerRepository = new CustomerRepository(_dataContext);
            var customerEntity = new CustomerEntity { Id = 1, FirstName = "Marcus", LastName = "Test", Email = "test@test.se", Password = "test", AddressId = 0 };
            await customerRepository.Create(customerEntity);

            //Act
            var result = customerRepository.DeleteCustomer("test@test.se");

            //Assert
            Assert.True(result);
        }
    }
}
