using Share.Entities;
using Share.Repositories;
using System.Diagnostics;

namespace Share.Services
{
    public class OrderService
    {
        private readonly OrderRepository _orderRepository;

        public OrderService(OrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        //Create
        public async Task<bool> AddOrder(ProductEntity product, CustomerEntity customer, int quantity)
        {
            try 
            {
                if (product != null && quantity > 0)
                {
                    var order = new Order
                    {
                        CustomerId = customer.Id,
                        TotalPrice = product.Price * quantity,
                        OrderDate = DateTime.Now,
                        OrderDetails = new OrderDetail
                        {
                            ProductId = product.ArticleNumber,
                            Quantity = quantity,
                            UnitPrice = product.Price,
                        }
                    };

                    var result = await _orderRepository.AddOrder(order);

                    if (result == true)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex) {Debug.WriteLine("AddOrder OrderServices" + ex.Message); 
            return false; 
            } 
        }

        //Read
        public async Task<IEnumerable<Order>> GetAllOrders()
        {
            try
            {
                var result = await _orderRepository.GetAllOrders();

                if(result != null)
                {
                    return result;
                }
                return null!;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("GetAllOrders OrderServices" + ex.Message);
            }
            return null!;
        }

        public async Task<Order> GetAnOrder(int orderId)
        {
            try
            {
                var result = await _orderRepository.GetAOrder(orderId);

                if(result != null)
                {
                    return result!;
                }
                return null!;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("GetAnOrders OrderServices" + ex.Message);
            }
            return null!;
        }


        //Update
        public async Task<bool> UpdateOrder(Order order, Action<Order> updateAction)
        {
            try
            {
                var existingOrder = await _orderRepository.GetAOrder(order.Id);

                if (existingOrder != null)
                {
                    updateAction(existingOrder);
                    var result = await _orderRepository.EditOrder(existingOrder);
                    
                    if (result == true)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("UpdateOrder OrderServices" + ex.Message);
            }
            return false;
        }

        //Delete
        public async Task<bool> DeleteOrder(int ordId) 
        {
            try
            {
                var result = await _orderRepository.DeleteOrder(ordId);

                if(result == true) 
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex) { Debug.WriteLine("DeleteOrder OrderSverives" + ex.Message);
                return false;
            }
        }




    }
}
