namespace UI;

public class LoginMenu : IMenu
{
    private IBL _bl;

    public LoginMenu(IBL bl)
    {
        _bl = bl;
    }

    public void Start()
    {
        List<Customer> allCustomers = _bl.GetAllCustomers();
        Console.WriteLine("Welcome back! Please log in: ");
        Console.WriteLine("Name: ");
        string username = Console.ReadLine();
        Console.WriteLine("Password: ");
        string password = Console.ReadLine();
        Customer returnCustomer = new Customer
            {
                UserName = username,
                Password = password,
            };
        int custID = _bl.GetCustomerID(username);
        Customer.CId = custID;
        bool ifUsername = allCustomers.Exists(x => x.UserName == returnCustomer.UserName);
        bool ifPassword = allCustomers.Exists(x => x.Password == returnCustomer.Password);
        if (ifUsername && ifPassword)
        {
            CurrentContext.currentCustomer = returnCustomer;
            Console.WriteLine($"Welcome back, {returnCustomer.UserName}!");
            MenuFactory.GetMenu("customer").Start();
        }
        else
        {
            Console.WriteLine("I couldn't find your information.");
            MenuFactory.GetMenu("main").Start();
        }
    }
}