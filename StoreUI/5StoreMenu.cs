namespace UI;
public class StoreMenu : IMenu
{
    private IBL _bl;
    public StoreMenu(IBL bl)
    {
        _bl = bl;
    }
    public void Start()
    {
        Console.WriteLine("Choose your preferred location: ");
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
                MenuFactory.GetMenu("earth").Start();
            break;
            case "2":
                MenuFactory.GetMenu("centauri").Start();
            break;
            default:
                Console.WriteLine("Please enter a valid number");
            break;
        }
    }
}