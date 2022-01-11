using System.Linq;
namespace UI;

public class EarthMenu : IMenu
{
    private IBL _bl;
    public EarthMenu(IBL bL)
    {
        _bl = bL;
    }
    public void Start()
    {
        Random rand = new Random();
        int orderID = rand.Next(1, 500);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Welcome to Earth! What Would you like to do?");
        Console.ResetColor();
        Console.WriteLine("[1] View products and place an order");
        Console.WriteLine("[2] Return to Main Menu");
        string response = Console.ReadLine();
        switch (response)
        {
            case "1":
                bool exit = false;
                while (!exit)
                {
                    Console.WriteLine("Welcome to Earth! \n\nPlease select from the following products: \n");
                    Storefront earth = CurrentContext.currentStore;
                    int storeID = CurrentContext.currentStore.StoreID;
                    List<Product> allProducts = _bl.GetAllEarthProducts();
                    for (int i = 0; i < allProducts.Count; i++)
                    {
                        Console.WriteLine($"\n[{i}] {allProducts[i].ProductName}: \n{allProducts[i].Description}\nPrice:\t${allProducts[i].Price}");
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
                        ProductID = selectedprod.ProductID,
                        OrderId = orderID
                    };

                    if(CurrentContext.lineItems == null)
                    {
                        CurrentContext.lineItems = new List<LineItem>();
                    }
                    CurrentContext.lineItems.Add(newLI);

                    if(CurrentContext.Cart == null)
                    {
                        //They are adding item to the cart for the first time

                        Order newOrder = new Order{OrderNumber = orderID, StoreId = CurrentContext.currentStore.StoreID, CustomerId = Customer.CId};
                        CurrentContext.Cart = newOrder;
                    }
                    // // listofLI.Add(newLI);
                    // for (int i = 0; i < CurrentContext.Cart.Count; i++)
                    // {
                    //     _bl.AddOrder(CurrentContext.Cart[i]);
                    //     _bl.AddLineItem(newLI,orderID);
                        
                    // }
                    // CurrentContext.Cart.LineItems.Add(newLI);
                    //clear the cart after ordering, to place a new order:
                    //CurrentContext.Cart = new Order();
                    Console.WriteLine("Keep Shopping or Place Order?");
                    Console.WriteLine("[1] Keep Shopping!\t[2] Place Order");
                    int shopInput = Int32.Parse(Console.ReadLine());
                    
                    if (shopInput == 1)
                    {

                    } 
                    else
                    {
                        _bl.AddOrder(CurrentContext.Cart);
                        foreach (LineItem item in CurrentContext.lineItems)
                        {
                            _bl.AddLineItem(item, orderID);
                        }
                        System.Console.WriteLine("Thank you for placing your order! You can find your order details in your customer account.");
                        exit = true;
                    }

                    //Somewhere after they're done picking out products,
                    //Ask if they want to place the order
                    //And then 
                    //_bl.AddOrder(CurrentContext.Cart)
                    //foreach (LineItem item in CurrentContext.lineItems)
                    //{
                    //  _bl.AddLineItem(item);
                    //}
                }
                // Console.WriteLine("Are you ready to place your order?");
                // System.Console.WriteLine("[1] Yes \t[2] No");
                // int orderInput = int.Parse(Console.ReadLine());

                // if(orderInput == 1)
                // {
                // }
                // else
                // {
                //     MenuFactory.GetMenu("store").Start();
                // }
                MenuFactory.GetMenu("customer").Start();

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
