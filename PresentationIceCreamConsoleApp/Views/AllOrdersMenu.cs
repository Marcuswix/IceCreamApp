using Microsoft.IdentityModel.Tokens;
using Share.Entities;
using Share.Repositories;

namespace PresentationIceCreamConsoleApp.Views
{
    public class AllOrdersMenu
    {
        private readonly OrderRepository _orderRepository;

        public AllOrdersMenu(OrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task AllOrdersMenuShow()
        {

            var orders = await _orderRepository.GetAllOrders();


            Console.Clear();
            Console.WriteLine("All Orders");
            Console.WriteLine("##########\n");

            foreach (var o in orders)
            {
                Console.WriteLine($"Order.Id: {o.Id}\nCustomer-Id: {o.CustomerId}\nProduct-Id: {o.OrderDetails.ProductId}\nQuantity: {o.OrderDetails.Quantity}\nTotal Price: {o.TotalPrice}\nOrder-date: {o.OrderDate}\n");
                Console.WriteLine("################\n");
            }

            Console.WriteLine("Press any key to contiune...");
            Console.ReadKey();
            Console.Clear();
            Console.SetCursorPosition(0, 0);

        }
    }
}
