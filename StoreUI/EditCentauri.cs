namespace UI;
public class EditCentauri : IMenu
{
    private IBL _bl;
    public EditCentauri(IBL bl)
    {
        _bl = bl;
    }
    public void Start()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        System.Console.WriteLine("========= Alpha Centauri ========");
        Console.ResetColor();
        System.Console.WriteLine("What would you like to do?");
        System.Console.WriteLine("[1] View All Product Inventory");
        System.Console.WriteLine("[2] Edit Product Inventory");
        System.Console.WriteLine("[3] View Order History");
        System.Console.WriteLine("[4] Return to Admin Menu");

        Storefront centauri = CurrentContext.currentStore;
        List<Product> allProducts = _bl.GetAllCentauriProducts();
        List<Inventory> allInventory = _bl.GetCentauriInventory();
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
                System.Console.WriteLine("[1] Add New Product \n[2] Delete Product\n[3] Replenish Inventory");

                switch (Console.ReadLine())
                {
                    case "1":
                        System.Console.WriteLine("Product Name: ");
                        string name = Console.ReadLine();
                        System.Console.WriteLine("Product Description: ");
                        string describe = Console.ReadLine();
                        System.Console.WriteLine("Price: "); 
                        decimal price = decimal.Parse(Console.ReadLine());
                        
                        Product newProd = new Product{
                            ProductName = name,
                            Description = describe,
                            Price = price
                        };
                        _bl.AddProduct(newProd);
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
                            MenuFactory.GetMenu("editcentauri").Start();
                        }
                    break;
                    case "2":
                        System.Console.WriteLine("Select a product to remove: ");
                        foreach (var (item, index) in prodInventory.Select((value, i) => (value, i)))
                        {
                            System.Console.WriteLine($"\n[{item.Product.ProductID}] {item.Product.ProductName}");
                        }
                        int prodID = int.Parse(Console.ReadLine());
                        _bl.RemoveProduct(prodID);
                        System.Console.WriteLine("Your product has been deleted!");
                        System.Console.WriteLine("[1] View New Inventory\n[2] Go Back");
                        int choice1 = int.Parse(Console.ReadLine());
                        if(choice1 == 1)
                        {
                            foreach (var (item, index) in prodInventory.Select((value, i) => (value, i)))
                            {
                                System.Console.WriteLine($"\n[{index}] {item.Product.ProductName}: {item.Product.Description}\nPrice: ${item.Product.Price}\tQuantity: {item.Inventory.Quantity}");
                            }
                        }
                        else
                        {
                            MenuFactory.GetMenu("editcentauri").Start();
                        }
                    break;
                    case "3":
                        bool exit = false;
                        while (!exit)
                        {
                            System.Console.WriteLine("Replenish Inventory: ");
                            System.Console.WriteLine("Select which item you would like to restock: ");
                            foreach (var (item, index) in prodInventory.Select((value, i) => (value, i)))
                            {
                                System.Console.WriteLine($"\n[{item.Product.ProductID}] {item.Product.ProductName}: {item.Product.Description}\nPrice: ${item.Product.Price}\tQuantity: {item.Inventory.Quantity}");
                            }
                            prodID = int.Parse(Console.ReadLine());
                            System.Console.WriteLine("How many items would you like to add?");
                            int quant = int.Parse(Console.ReadLine());
                            _bl.RestockCentauriInventory(prodID, quant);
                            System.Console.WriteLine("\n[1] Change more inventory\n[2] Go Back to Main Menu");
                            string response =Console.ReadLine();
                            if (int.Parse(response) == 1)
                            {
                                
                            }
                            else if (int.Parse(response) == 2)
                            {
                                MenuFactory.GetMenu("editcentauri").Start();
                            }
                            else if (string.IsNullOrEmpty(response));
                            {
                                System.Console.WriteLine("Invalid Response");
                                exit = true;
                            }
                        }    
                    break;
                    default:
                    break;
                }

            break;
            case "3":
                System.Console.WriteLine("View Order History For Area 51: ");
                List<Order> allOrders = _bl.GetAllCentauriOrders();
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