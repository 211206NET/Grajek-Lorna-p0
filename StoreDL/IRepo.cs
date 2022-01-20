global using System.Text.Json;
global using Models;
namespace StoreDL;

public interface IRepo
{
    List<Customer> GetAllCustomers();
    List<Customer> SearchCustomer(string username, string password);
    void AddCustomer(Customer newCustomer);
    Customer GetCustomerById(int custId);
    //---------------------------------------------------------------------------------------------------------------------------------
    List<Storefront> GetAllStores();
    void AddLineItem(LineItem newLI, int orderID);
    List<Product> GetAllEarthProducts();
    List<Product> GetAllCentauriProducts();
    void AddStore(Storefront storetoAdd);
    void AddOrder(Order orderToAdd);
    List<Order> GetAllOrders(int CID);
    List<Inventory> GetEarthInventory();
    void AddProduct(Product productToAdd);
    void RemoveProduct(int prodID);
    void RestockEarthInventory(int prodID, int quantity);
    List<Order> GetAllEarthOrders();
    List<Order> GetAllCentauriOrders();
    List<Inventory> GetCentauriInventory();
    void RestockCentauriInventory(int prodID, int quantity);
    int GetProductID(string productname);
    void AddProductToInventory(int prodID, Inventory inventToAdd);
    Storefront GetStorefrontById(int storeID);
}