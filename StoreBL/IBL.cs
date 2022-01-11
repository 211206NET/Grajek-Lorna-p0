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
}