global using System.Text.Json;
global using Models;
namespace StoreDL;

public interface IRepo
{
    List<Storefront> GetAllStores();
    List<Customer> GetAllCustomers();
    void AddCustomer(Customer newCustomer);
    void AddLineItem(LineItem newLI);
    List<Product> GetAllEarthProducts();
    void AddStore(Storefront storetoAdd);
    void AddOrder(Order orderToAdd);
    int GetCustomerID(string username);

}