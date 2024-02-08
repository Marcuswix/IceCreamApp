using Share.Contexts;
using Share.Dtos;
using Share.Entities;
using Share.Helpers;
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

        //Create
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
                        Password = PasswordHandler.GenerateSecurePassword(customer.Password),
                        Address = new AddressEntity
                        {
                                StreetName = customer.StreetName,
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

        //Read
        public async Task<IEnumerable<Customer>> GetAllCustomers()
        {
            var customers = new List<Customer>();

            try
            {
                var result = await _customerRepository.GetAll();

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
                            var address = await _addressRepository.GetAll();

                            if (address != null)
                            {
                            customer.StreetName = c.Address.StreetName;
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

        public async Task<CustomerEntity> GetACustomer(string email)
        {
            try 
            {
                if(email != null)
                {
                    var result = await _customerRepository.GetOne(x => x.Email == email);
                    return result;
                }
                return null!;
            }
            catch (Exception ex) { Debug.WriteLine("GetACustomer" + ex.Message);
                return null!;
            }
        }

        //Delete
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

        //Update
        public async Task<bool> UpdateCustomer(string email, Action<CustomerEntity> updateAction)
        {
            try
            {
                var existingCustomer = await _customerRepository.GetOne(x => x.Email == email);

                if (existingCustomer != null)
                {
                    updateAction(existingCustomer);

                    if(!string.IsNullOrEmpty(existingCustomer.Password))
                    {
                        existingCustomer.Password = PasswordHandler.GenerateSecurePassword(existingCustomer.Password);
                    }

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
