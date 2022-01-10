namespace StoreBL;

public interface IBL
{
    List<Storefront> GetAllStores();
    List<Customer> GetAllCustomers();
    void AddCustomer(Customer newCustomer);
    List<Product> GetAllEarthProducts();
    List<Product> GetAllCentauriProducts();
    void AddLineItem(LineItem newLI);
    void AddStore(Storefront storetoAdd);
    void AddOrder(Order orderToAdd);
    int GetCustomerID(string username);
}