namespace UI;

public static class MenuFactory
{
    public static IMenu GetMenu(string menuString)
    {
        menuString = menuString.ToLower();
        string connectionString = File.ReadAllText("connectionString.txt");
        IRepo repo = new DBRepo(connectionString);
        IBL bl = new UFOBL(repo);

        switch(menuString)
        {
            case "main":
                return new MainMenu(bl);
            
            case "login":
                return new LoginMenu(bl);

            case "customer":
                return new CustomerMenu(bl);
            
            case "store":
                return new StoreMenu(bl);

            case "earth":
                return new EarthMenu(bl);
            
            case "centauri":
                return new CentauriMenu(bl);

            case "manager":
                return new ManagerMenu(bl);
            
            case "editearth":
                return new EditEarth(bl);
            
            case "editcentauri":
                return new EditCentauri(bl);

            default:
                return new MainMenu(bl);
        }
    }
}