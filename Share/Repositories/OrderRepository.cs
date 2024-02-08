using Microsoft.EntityFrameworkCore;
using Share.Contexts;
using Share.Entities;
using System.Diagnostics;

namespace Share.Repositories
{
    public class OrderRepository
    {
        private readonly DataContextOrders _contextOrders;

        public OrderRepository(DataContextOrders contextOrders)
        {
            _contextOrders = contextOrders;
        }

        public async Task <bool> AddOrder(Order order) 
        {
            if (order != null) 
            {
                try
                {
                    await _contextOrders.Orders.AddAsync(order);
                    await _contextOrders.SaveChangesAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Addorder" + ex.Message);
                    return false;
                }
            }
            return false;
        }

        public async Task<Order> GetAOrder(int orderToFind)
        {
            try
            {
                var result = await _contextOrders.Orders.FirstOrDefaultAsync(x => x.Id == orderToFind);

                if (result != null) 
                {
                    return result;
                }
                return null!;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("DeleteOrder" + ex.Message);
                return null!;
            }
        }

        public async Task<bool> DeleteOrder(int orderId) 
        {
            var orderToDelete = await GetAOrder(orderId);

            try
            {
                if (orderToDelete != null)
                {
                    _contextOrders.Remove(orderToDelete);
                    await _contextOrders.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex) { Debug.WriteLine("DeleteOrder" + ex.Message);
            return false;
            }
        }

        public async Task<bool> EditOrder(Order order)
        {
            try 
            {
                var orderToUpdate = _contextOrders.Orders.FirstOrDefault(o => o.Id == order.Id);

                if(_contextOrders != null)
                {
                    _contextOrders.Entry(order).CurrentValues.SetValues(orderToUpdate!);
                    await _contextOrders.SaveChangesAsync();
                    return true;
                }
                return false;

            }
            catch(Exception ex) { Debug.WriteLine("Editorder" + ex.Message);
            return true;
            }
        }

        public async Task<IEnumerable<Order>> GetAllOrders()
        {
            var list = new List<Order>();

            try
            {
                if(_contextOrders.Orders != null)
                {

                    list = await _contextOrders.Orders
                        .Include(c => c.OrderDetails)
                        .ToListAsync();

                    return list;
                }
                return null!;

            }
            catch (Exception ex) { Debug.WriteLine("GetAllOrders" + ex.Message);
            return null!;
            }
        }
    }
}
