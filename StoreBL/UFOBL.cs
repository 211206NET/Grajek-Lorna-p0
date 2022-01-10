namespace StoreBL;

public class UFOBL : IBL
{
    private IRepo _dl;
    public UFOBL(IRepo repo)
    {
        _dl = repo;
    }

    public List<Storefront> GetAllStores()
    {
        return _dl.GetAllStores();
    }

    public List<Customer> GetAllCustomers()
    {
        return _dl.GetAllCustomers();
    }

    public void AddCustomer(Customer newCustomer)
    {
        _dl.AddCustomer(newCustomer);
    }
    public List<Product> GetAllEarthProducts()
    {
        return _dl.GetAllEarthProducts();
    }
    public List<Product> GetAllCentauriProducts()
    {
        return _dl.GetAllCentauriProducts();
    }
    public void AddLineItem(LineItem newLI)
    {
        _dl.AddLineItem(newLI);
    }
    public void AddStore(Storefront storetoAdd)
    {
        _dl.AddStore(storetoAdd);
    }
    public void AddOrder(Order orderToAdd)
    {
        _dl.AddOrder(orderToAdd);
    }
    public int GetCustomerID(string username)
    {
        return _dl.GetCustomerID(username);
    }
}