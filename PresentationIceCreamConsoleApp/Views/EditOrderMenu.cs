using Share.Entities;
using Share.Repositories;
using Share.Services;

namespace PresentationIceCreamConsoleApp.Views
{
    public class EditOrderMenu
    {
        private readonly OrderRepository _orderRepository;
        private readonly OrderService _orderService;

        public EditOrderMenu(OrderRepository orderRepository, OrderService orderService)
        {
            _orderRepository = orderRepository;
            _orderService = orderService;
        }

        public async Task EditOrderMenuShow()
        {
            Console.Clear();
            Console.WriteLine("Edit product");
            Console.WriteLine("############");
            Console.WriteLine("Write the order-id to edit an order: ");
            var order = Console.ReadLine();

            if (order != null)
            {
                int orderToEdit = int.Parse(order);

                var TheOrder = await _orderRepository.GetAOrder(orderToEdit);

                var result = await _orderService.UpdateOrder(TheOrder, editedOrder =>
                {
                    Console.WriteLine("New Customer ID: ");
                    editedOrder.CustomerId = int.Parse(Console.ReadLine()!);
                    Console.WriteLine("New Total price: ");
                    editedOrder.TotalPrice = int.Parse(Console.ReadLine()!);

                    if (editedOrder.OrderDetails == null)
                    {
                        editedOrder.OrderDetails = new OrderDetail();
                    }
                    Console.WriteLine("New Article Number: ");
                    editedOrder.OrderDetails.ProductId = Console.ReadLine()!;
                    Console.WriteLine("New quantity: ");
                    editedOrder.OrderDetails.Quantity = int.Parse(Console.ReadLine()!);
                    Console.WriteLine("New unit price: ");
                    editedOrder.OrderDetails.UnitPrice = int.Parse(Console.ReadLine()!);
                    editedOrder.OrderDate = DateTime.Now;
                });

                if (result == true)
                {
                    Console.WriteLine("The Order is now updated!");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }

                else
                {
                    Console.WriteLine("Something went wrong.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
            }
            else
            {
                Console.WriteLine("Not a valid order-id");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }
    }
}
