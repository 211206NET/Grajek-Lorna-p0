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
        Console.WriteLine("Welcome to Earth! \nPlease select from the following products: ");
        Storefront earth = CurrentContext.currentStore;
        int storeID = CurrentContext.currentStore.StoreID;
        List<Product> allProducts = _bl.GetAllEarthProducts();
        for (int i = 0; i < allProducts.Count; i++)
        {
            Console.WriteLine($"[{i}] {allProducts[i].ProductName}: \n{allProducts[i].Description}\n");
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
        //clear the cart after ordering, to place a new order:
        //CurrentContext.Cart = new Order();
    }
}
