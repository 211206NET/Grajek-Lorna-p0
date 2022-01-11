namespace UI;

public class CentauriMenu : IMenu
{
    private IBL _bl;
    public CentauriMenu(IBL bL)
    {
        _bl = bL;
    }
    public void Start()
    {
        Random rand = new Random();
        int orderID = rand.Next(1, 500);
        LineItem.OrderId = orderID;
        bool exit = false;
        while (!exit)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Welcome to Proxima B! What Would you like to do?");
            Console.ResetColor();
            Console.WriteLine("[1] View products and place an order");
            Console.WriteLine("[2] Return to Main Menu");
            string response = Console.ReadLine();
            
            switch (response)
            {
                case "1":
                    CreateLineItem();
                break;
                case "2":
                    MenuFactory.GetMenu("store").Start();
                break;
                default:
                    MenuFactory.GetMenu("store").Start();
                break;
            }
        }
    }
    public void CreateLineItem()
    {
        Console.WriteLine("\nPlease select from the following products: ");
                        Storefront centauri = CurrentContext.currentStore;
                        int storeID = CurrentContext.currentStore.StoreID;
                        List<Product> allProducts = _bl.GetAllCentauriProducts();

                        for (int i = 0; i < allProducts.Count; i++)
                        {
                            Console.WriteLine($"[{i}] {allProducts[i].ProductName}: \n{allProducts[i].Description}\nPrice:\t{allProducts[i].Price}");
                        }

                        int input = int.Parse(Console.ReadLine());
                        Product selectedprod = new Product();
                        selectedprod = allProducts[input];

                        Console.WriteLine($"How many {selectedprod.ProductName} would you like to buy?");
                        int input2 = int.Parse(Console.ReadLine());

                        int prodID = selectedprod.ProductID;
                        LineItem newLI = new LineItem
                        {
                            Item = selectedprod,
                            Quantity = input2,
                            ProductID = selectedprod.ProductID
                        };
                        List<LineItem> listofLI = new List<LineItem>();
                        listofLI.Add(newLI);
                        CurrentContext.Cart = new Order{OrderNumber = LineItem.OrderId, StoreId = CurrentContext.currentStore.StoreID, CustomerId = Customer.CId};
                        CurrentContext.Cart.LineItems.Add(newLI);
                        // _bl.AddOrder(CurrentContext.Cart);
                        _bl.AddLineItem(newLI, LineItem.OrderId);
    }
}