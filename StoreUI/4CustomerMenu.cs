namespace UI;
public class CustomerMenu : IMenu
{
    private IBL _bl;
    public CustomerMenu(IBL bl)
    {
        _bl = bl;
    }
    public void Start()
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("=============================");
        Console.ResetColor();
        Console.WriteLine("Boarding the Mothership");
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("=============================");
        Console.ResetColor();
        Console.WriteLine("What would you like to do today?");
        Console.WriteLine("[1] Place an order");
        Console.WriteLine("[2] View order history");
        Console.WriteLine("[3] Return to main menu");
        string input = Console.ReadLine();

        switch (input)
        {
            case "1":
                MenuFactory.GetMenu("store").Start();
            break;
            case "2":
                int ID = Customer.CId;
                List<Order> allOrders = _bl.GetAllOrders(ID);
                Console.WriteLine($"Order History for Customer #{ID}");
                foreach (Order order in allOrders)
                {
                    Console.WriteLine($"Order Number: {order.OrderNumber}  Amount: {order.Total}  Date: {order.OrderDate}");
                }
                MenuFactory.GetMenu("customer").Start();
            break;
            case "3":
                MenuFactory.GetMenu("main").Start();
            break;
            default:
                Console.WriteLine("I don't understand your alien language, try again");
                MenuFactory.GetMenu("main").Start();
            break;
        }
    }
}