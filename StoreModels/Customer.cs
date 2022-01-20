global using System.Data;
namespace Models;

public class Customer
{
    public Customer()
    {
        this.Orders = new List<Order>();
    }
    public Customer(DataRow row)
    {
        CId = (int) row["CustomerId"];
        this.UserName = row["UserName"].ToString() ?? "";
        this.Password = row["PassWord"].ToString() ?? "";
    }
    public static int CId { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public List<Order> Orders { get; set; }
}