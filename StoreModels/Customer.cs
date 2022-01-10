namespace Models;

public class Customer
{
    public Customer()
    {
        this.Orders = new List<Order>();
    }
    public static int CId { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public List<Order> Orders { get; set; }
}