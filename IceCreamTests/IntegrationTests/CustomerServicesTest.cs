using Moq;
using Share.Dtos;
using Share.Repositories;
using Share.Services;

namespace IceCreamTests.IntegrationTests
{
    public class CustomerServicesTest
    {
        private readonly CustomerService _customerService;
        private readonly CustomerRepository _customerRepository;
        private readonly AddressRepository _addressRepository;
        private readonly Customer customer;

        public CustomerServicesTest(CustomerService customerService, Customer customer, CustomerRepository customerRepository, AddressRepository addressRepository)
        {
            _customerService = customerService;
            this.customer = customer;
            _customerRepository = customerRepository;
            _addressRepository = addressRepository;
        }

        //[Fact]
        //public void CreateCustomer_ShouldCreateACustomer_ThenReturnTrue()
        //{
        //    //Arrange
        //    var customerRepositoryMock = new Mock<CustomerRepository>();
        //    var customerService = new CustomerService(customerRepositoryMock.Object);
        //    var customer = new Customer { FirstName = "Test", LastName = "Test", Email = "test", Password = "test", City = "Test", PostalCode = 12345, SteetName = "Test", Id = 0 };

        //    //Act
        //    var result = _customerService.CreateCustomer(customer);

        //    //Assert
        //    Assert.True(result);
        //}
    }
}
