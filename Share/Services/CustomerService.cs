using Share.Contexts;
using Share.Dtos;
using Share.Entities;
using Share.Repositories;
using System.Diagnostics;

namespace Share.Services
{
    public class CustomerService
    {
        private readonly AddressRepository _addressRepository;
        private readonly CustomerRepository _customerRepository;
        private readonly DataContext _dataContext;

        public CustomerService(CustomerRepository customerRepository, AddressRepository addressRepository, DataContext dataContext)
        {
            _customerRepository = customerRepository;
            _addressRepository = addressRepository;
            _dataContext = dataContext;
        }

        public bool CreateCustomer(Customer customer)
        {
            try
            {
                var validCustomer = customer.Email;

                if (validCustomer == null || customer.Email == string.Empty) 
                {
                    return false;
                }

                    if (!_customerRepository.Exists(x => x.Email == customer.Email))
                    {
                        var customerEntity = new CustomerEntity
                        {
                            Id = customer.Id,
                            FirstName = customer.FirstName,
                            LastName = customer.LastName,
                            Email = customer.Email,
                            Password = customer.Password,
                            Address = new AddressEntity
                            {
                                SteetName = customer.SteetName,
                                PostalCode = customer.PostalCode,
                                City = customer.City,
                            }

                        };

                        var result = _customerRepository.Create(customerEntity);

                        if (result != null)
                        {
                            return true;
                        }
                    }
            }
            catch (Exception ex) { Debug.WriteLine("CreateCustomerProdServ" + ex.Message); }

            return false;
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            var customers = new List<Customer>();

            try
            {
                var result = _customerRepository.GetAll();

                foreach (var c in result)
                {
                    var customer = new Customer
                    {
                        Id = c.Id,
                        FirstName = c.FirstName,
                        LastName = c.LastName,
                        Email = c.Email,
                        Password = c.Password
                    };
                        
                        if (c.AddressId != null)
                        {
                            var address = _addressRepository.GetAll();

                            if (address != null)
                            {
                            customer.SteetName = c.Address.SteetName;
                            customer.PostalCode = c.Address.PostalCode;
                            customer.City = c.Address.City;
                            }
                        }
                    customers.Add(customer);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("GetAllCustomersService: " + ex.Message);
            }

            return customers;
        }


        public bool DeleteCustomer(string email)
        {
            try
            {
                if (!string.IsNullOrEmpty(email))
                {
                    var result = _customerRepository.DeleteCustomer(email);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("DeleteCustomerCustomerService" + ex.Message);
                return false;
            }
        }

        public bool UpdateCustomer(string email, Action<CustomerEntity> updateAction)
        {
            try
            {
                var existingCustomer = _customerRepository.GetOne(x => x.Email == email);

                if (existingCustomer != null)
                {
                    updateAction(existingCustomer);
                    var result = _customerRepository.UpdateCustomer(existingCustomer);
                    if (result != null)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("UpdateCustomerProdServ" + ex.Message);
            }

            return false;
        }

    }
}
