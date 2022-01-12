namespace UI;
public class ManagerMenu : IMenu
{
    private IBL _bl;
    public ManagerMenu(IBL bl)
    {
        _bl = bl;
    }
    public void Start()
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("\n=============================");
        Console.ResetColor();
        Console.WriteLine("Admin Mode Inialized");
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("=============================");
        Console.ResetColor();
        Console.WriteLine("What would you like to do today?");
        Console.WriteLine("[1] Add a new location");
        Console.WriteLine("[2] View all locations");
        Console.WriteLine("[3] Return to main menu");
        string input = Console.ReadLine();

        switch (input)
        {
            case "1":
                Console.WriteLine("\nStore Name: ");
                string name = Console.ReadLine();
                Console.WriteLine("\nStore Address: ");
                string address = Console.ReadLine();
                Random rand = new Random();
                int storeID = rand.Next(3, 10);
                Storefront newStore = new Storefront
                {
                    StoreID = storeID,
                    Address = address,
                    Name = name,
                };
            _bl.AddStore(newStore);
            System.Console.WriteLine("Store Added!");
            MenuFactory.GetMenu("manager").Start();
            break;
            case "2":
                Console.WriteLine("Select a store to see more information: ");
                List<Storefront> allStores = _bl.GetAllStores();
                for (int i = 0; i < allStores.Count; i++)
                {
                    Console.WriteLine($"\n[{i + 1}] {allStores[i].Name} located on {allStores[i].Address}");
                }
                string selection = Console.ReadLine();
                CurrentContext.currentStore = allStores[int.Parse(selection)];
                switch (selection)
                {
                    case "1":
                        MenuFactory.GetMenu("editearth").Start();
                    break;
                    case "2":
                        MenuFactory.GetMenu("editcentauri").Start();
                    break;
                    default:
                    break;
                }
            break;
            default:
            break;
        }
    }
}