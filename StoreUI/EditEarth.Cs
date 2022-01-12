namespace UI;
public class EditEarth : IMenu
{
    private IBL _bl;
    public EditEarth(IBL bl)
    {
        _bl = bl;
    }
    public void Start()
    {
        Console.ForegroundColor = ConsoleColor.Green;
        System.Console.WriteLine("========= Earth ========");
        Console.ResetColor();
        System.Console.WriteLine("What would you like to do?");
        System.Console.WriteLine("[1] View All Product Inventory");
        System.Console.WriteLine("[2] Edit Product Inventory");
        System.Console.WriteLine("[3] View Order History");
        System.Console.WriteLine("[4] Return to Admin Menu");

        Storefront earth = CurrentContext.currentStore;
        List<Product> allProducts = _bl.GetAllEarthProducts();
        List<Inventory> allInventory = _bl.GetEarthInventory();
        var prodInventory = allProducts.Zip(allInventory, (p, i) => new {Product = p, Inventory = i});
        switch (Console.ReadLine())
        {
            case "1":
                foreach (var (item, index) in prodInventory.Select((value, i) => (value, i)))
                {
                    System.Console.WriteLine($"\n[{index}] {item.Product.ProductName}: {item.Product.Description}\nPrice: ${item.Product.Price}\tQuantity: {item.Inventory.Quantity}");
                }
                System.Console.WriteLine("Press Enter to return to menu");
                Console.ReadKey();
                if (Console.ReadKey().Key == ConsoleKey.Enter)
                {
                    MenuFactory.GetMenu("editearth").Start();
                }
            break;
            case "2":
                System.Console.WriteLine("[1] Add New Product \n[2] Replenish Inventory");

                switch (Console.ReadLine())
                {
                    case "1":
                        System.Console.WriteLine("Product Name: ");
                        string name = Console.ReadLine();
                        System.Console.WriteLine("Product Description: ");
                        string describe = Console.ReadLine();
                        System.Console.WriteLine("Price: "); 
                        decimal price = decimal.Parse(Console.ReadLine());
                        System.Console.WriteLine("Quantity: ");
                        int quant = int.Parse(Console.ReadLine());
                        
                        Product newProd = new Product{
                            ProductName = name,
                            Description = describe,
                            Price = price
                        };
                        _bl.AddProduct(newProd);
                        int productID = _bl.GetProductID(newProd.ProductName);
                        Inventory newInvent = new Inventory{
                            StoreId = CurrentContext.currentStore.StoreID,
                            Quantity = quant,
                            ProductID = productID
                        };
                        _bl.AddProductToInventory(productID, newInvent);
                        System.Console.WriteLine($"{newProd.ProductName} has been added!\n[1] View New Inventory\n[2] Go Back");
                        int choice = int.Parse(Console.ReadLine());
                        if(choice == 1)
                        {
                            foreach (var (item, index) in prodInventory.Select((value, i) => (value, i)))
                            {
                                System.Console.WriteLine($"\n[{index}] {item.Product.ProductName}: {item.Product.Description}\nPrice: ${item.Product.Price}\tQuantity: {item.Inventory.Quantity}");
                            }
                        }
                        else 
                        {
                            MenuFactory.GetMenu("editearth").Start();
                        }
                    break;
                    case "2":
                        bool exit = false;
                        while (!exit)
                        {
                            System.Console.WriteLine("Replenish Inventory: ");
                            System.Console.WriteLine("Select which item you would like to restock: ");
                            foreach (var (item, index) in prodInventory.Select((value, i) => (value, i)))
                            {
                                System.Console.WriteLine($"\n[{item.Product.ProductID}] {item.Product.ProductName}: {item.Product.Description}\nPrice: ${item.Product.Price}\tQuantity: {item.Inventory.Quantity}");
                            }
                            int prodID = int.Parse(Console.ReadLine());
                            System.Console.WriteLine("How many items would you like to add?");
                            int quantity = int.Parse(Console.ReadLine());
                            _bl.RestockEarthInventory(prodID, quantity);
                            System.Console.WriteLine("\n[1] Change more inventory\n[2] Go Back to Main Menu");
                            string response =Console.ReadLine();
                            if (int.Parse(response) == 1)
                            {
                                
                            }
                            else if (int.Parse(response) == 2)
                            {
                                MenuFactory.GetMenu("editearth").Start();
                            }
                            else if (string.IsNullOrEmpty(response));
                            {
                                System.Console.WriteLine("Invalid Response");
                                exit = true;
                            }
                        }
                    break;
                    default:
                        MenuFactory.GetMenu("editearth").Start();
                    break;
                }
            break;
            case "3":
                System.Console.WriteLine("View Order History For Area 51: ");
                List<Order> allOrders = _bl.GetAllEarthOrders();
                foreach (Order o in allOrders)
                {
                    System.Console.WriteLine($"Customer ID: {o.CustomerId} Order Number: {o.OrderNumber} Total: {o.Total} Date: {o.OrderDate}");
                }
                System.Console.WriteLine("Press Enter to return to Store Menu");
                if (Console.ReadKey().Key == ConsoleKey.Enter)
                {
                    MenuFactory.GetMenu("editearth").Start();
                }

            break;
            case "4":
                MenuFactory.GetMenu("manager").Start();
            break;
            default:
                MenuFactory.GetMenu("manager").Start();
            break;
        }
    }
}