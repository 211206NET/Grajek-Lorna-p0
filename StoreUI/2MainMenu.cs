namespace UI;

public class MainMenu : IMenu
{
    private IBL _bl;
    public MainMenu(IBL bl)
    {
        _bl = bl;
    }
    public void Start()
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Welcome to the UFO Emporium!");
        Console.WriteLine("Home of all your intergalactic needs!");
        Console.ResetColor();
        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("Please Select from the Following: ");
            Console.WriteLine("[1] Log-in \t[2] Sign-Up\t[3] Exit");
            string input = Console.ReadLine();

            if(!string.IsNullOrWhiteSpace(input))
            {
                switch (input)
                {
                    case "1":
                        MenuFactory.GetMenu("login").Start();
                    break;
                    case "2":
                        Console.WriteLine("Greetings! What is your Earth name?");
                        string username = Console.ReadLine();
                        Console.WriteLine("Please enter a password: ");
                        string password = Console.ReadLine();
                        Random rand = new Random();
                        int custID = rand.Next(1, 1001);
                        Customer.CId = custID;
                        Customer newCustomer = new Customer
                            {
                                UserName = username,
                                Password = password,
                            };
                        
                        _bl.AddCustomer(newCustomer);
                        Console.WriteLine($"Thank you {newCustomer.UserName} for creating an account.");
                        MenuFactory.GetMenu("customer").Start();
                    break;
                    case "I am your leader":
                        //launch manager functions
                        Console.WriteLine("Initialize Admin Mode ");
                        MenuFactory.GetMenu("manager").Start();
                    break;
                    case "3":
                        Environment.Exit(3);
                    break;
                }
            }
            else
            {
                Console.WriteLine("Try Again");
            }
        }
    }
}
