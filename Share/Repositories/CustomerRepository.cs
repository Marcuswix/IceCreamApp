using Microsoft.EntityFrameworkCore;
using Share.Contexts;
using Share.Entities;
using Share.Dtos;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

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
                var entityToDelete = _context.Set<CustomerEntity>().FirstOrDefault(x => x.Email == email);

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

        public override bool Exists(Expression<Func<CustomerEntity, bool>> predicate)
        {
            try
            {
                var result = _context.Set<CustomerEntity>().Any(predicate);
                return result;
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
                var entityToUpdate = _context.Set<CustomerEntity>().Find(customer.Id);

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
