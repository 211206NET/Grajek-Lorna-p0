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
        Console.WriteLine("=============================");
        Console.ResetColor();
        Console.WriteLine("Admin Mode Inialized");
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("=============================");
        Console.ResetColor();
        Console.WriteLine("What would you like to do today?");
        Console.WriteLine("[1] Add a new location");
        Console.WriteLine("[2] View all locations");
        Console.WriteLine("[3] Return to main menu");
        string? input = Console.ReadLine();

        switch (input)
        {
            case "1":
                Console.WriteLine("\nStore Name: ");
                string? name = Console.ReadLine();
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
            break;
            case "2":
                Console.WriteLine("Select a store to edit the inventory: ");
                List<Storefront> allStores = _bl.GetAllStores();
                for (int i = 0; i < allStores.Count; i++)
                {
                    Console.WriteLine($"\n[{i}] {allStores[i].Name} located on {allStores[i].Address}");
                }
                string selection = Console.ReadLine();
                CurrentContext.currentStore = allStores[int.Parse(selection)];
            break;
            default:
            break;
        }
    }
}