using Share.Services;

namespace PresentationIceCreamConsoleApp.Views
{
    public class DeleteOrderMenu
    {
        private readonly OrderService _orderService;

        public DeleteOrderMenu(OrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task DeleteOrderMenuShow()
        {
            Console.Clear();
            Console.WriteLine("Delete Order");
            Console.WriteLine("############");
            Console.WriteLine("Write the orderId to delete a order: ");
            var input = Console.ReadLine();

            if (int.TryParse(input, out int orderToDelete))
            {
                var result = await _orderService.DeleteOrder(orderToDelete);

                if (result == true)
                {
                    Console.WriteLine("\nThe order is deleted. Press any key to continue...");
                    Console.ReadKey();
                }
                else
                {

                    Console.WriteLine("\nNo order is deleted. Press any key to continue...");
                    Console.ReadKey();
                }
            }
            else
            {
                Console.WriteLine("Not a valid order-Id...");
                Thread.Sleep(1000);
            }
        }
    }
}
