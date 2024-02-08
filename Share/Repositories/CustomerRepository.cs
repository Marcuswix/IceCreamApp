using Microsoft.EntityFrameworkCore;
using Share.Contexts;
using Share.Entities;
using System.Diagnostics;

namespace Share.Repositories
{
    public class CustomerRepository : BaseRepository<CustomerEntity>
    {
        private readonly DataContext _context;

        public CustomerRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public bool DeleteCustomer(string email)
        {
            try
            {
                var entityToDelete = _context.Customer.FirstOrDefault(x => x.Email == email);

                if (entityToDelete != null)
                {
                    _context.Remove(entityToDelete);
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("CustomerRepository Delete" + ex.Message);
                return false;
            }
        }

        public bool Exists(CustomerEntity customer)
        {
            try
            {
                _context.Customer.FirstOrDefault(c => c.Email == customer.Email);
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ExistsBaseRes" + ex.Message);
                return false;
            }
        }

        public CustomerEntity UpdateCustomer(CustomerEntity customer)
        {
            try
            {
                var entityToUpdate = _context.Customer.Find(customer.Id);

                if (entityToUpdate != null)
                {
                    _context.Entry(entityToUpdate).CurrentValues.SetValues(customer);
                    _context.SaveChanges();
                    return entityToUpdate;
                }

                return null!;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("UpdateCustomer: " + ex.Message);
                return null!;
            }
        }
    }
}
