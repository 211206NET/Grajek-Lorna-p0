namespace StoreBL;

public interface IBL
{
    List<Storefront> GetAllStores();
    List<Customer> GetAllCustomers();
    void AddCustomer(Customer newCustomer);
    List<Product> GetAllEarthProducts();
    List<Product> GetAllCentauriProducts();
    void AddLineItem(LineItem newLI, int orderID);
    void AddStore(Storefront storetoAdd);
    void AddOrder(Order orderToAdd);
    List<Order> GetAllOrders(int CID);
    int GetCustomerID(string username);
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

}