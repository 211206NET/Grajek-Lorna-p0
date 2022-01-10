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
        bool exit = false;
        while (!exit)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Welcome to Proxima B! What Would you like to do?");
            Console.ResetColor();
            Console.WriteLine("[1] View products and place an order");
            Console.WriteLine("[2] Return to Main Menu");
            string? response = Console.ReadLine();
            switch (response)
            {
                case "1":
                    while (!exit)
                    {
                        Console.WriteLine("\nPlease select from the following products: ");
                        Storefront centauri = CurrentContext.currentStore;
                        int storeID = CurrentContext.currentStore.StoreID;
                        //Change this
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
                        Random rand = new Random();
                        int orderID = rand.Next(1, 500);
                        int prodID = selectedprod.ProductID;
                        LineItem newLI = new LineItem
                        {
                            Item = selectedprod,
                            OrderId = orderID,
                            Quantity = input2,
                            ProductID = selectedprod.ProductID
                        };
                        List<LineItem> listofLI = new List<LineItem>();
                        CurrentContext.Cart = new Order{OrderNumber = orderID, StoreId = CurrentContext.currentStore.StoreID, CustomerId = Customer.CId};
                        listofLI.Add(newLI);
                        CurrentContext.Cart.LineItems.Add(newLI);
                        _bl.AddOrder(CurrentContext.Cart);
                        _bl.AddLineItem(newLI);
                        Console.WriteLine("[1] Add More Items");
                        //customer menu eventually
                        Console.WriteLine("[2] Return to menu");
                        string? response2 = Console.ReadLine();
                        switch (response2)
                        {
                            case "1":

                            break;
                            case "2":
                            //customer menu eventually
                                MenuFactory.GetMenu("store").Start();
                            break;
                            default:
                                MenuFactory.GetMenu("store").Start();
                            break;
                        }
                    }
                break;
                case "2":
                //change this later to a customer menu
                    CurrentContext.Cart = new Order();
                    MenuFactory.GetMenu("store").Start();
                break;
                default:
                    MenuFactory.GetMenu("store").Start();
                break;
            }
        }
    }
}