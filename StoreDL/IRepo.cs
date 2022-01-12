global using System.Text.Json;
global using Models;
namespace StoreDL;

public interface IRepo
{
    List<Storefront> GetAllStores();
    List<Customer> GetAllCustomers();
    void AddCustomer(Customer newCustomer);
    void AddLineItem(LineItem newLI, int orderID);
    List<Product> GetAllEarthProducts();
    List<Product> GetAllCentauriProducts();
    void AddStore(Storefront storetoAdd);
    void AddOrder(Order orderToAdd);
    List<Order> GetAllOrders(int CID);
    int GetCustomerID(string username);
    List<Inventory> GetEarthInventory();
    void AddProduct(Product productToAdd);
    void RemoveProduct(int prodID);
    void RestockEarthInventory(int prodID, int quantity);
}