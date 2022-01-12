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
    public void AddLineItem(LineItem newLI, int orderID)
    {
        _dl.AddLineItem(newLI, orderID);
    }
    public void AddStore(Storefront storetoAdd)
    {
        _dl.AddStore(storetoAdd);
    }
    public void AddOrder(Order orderToAdd)
    {
        _dl.AddOrder(orderToAdd);
    }
    public List<Order> GetAllOrders(int CID)
    {
        return _dl.GetAllOrders(CID);
    }
    public int GetCustomerID(string username)
    {
        return _dl.GetCustomerID(username);
    }
    public List<Inventory> GetEarthInventory()
    {
        return _dl.GetEarthInventory();
    }
    public void AddProduct(Product productToAdd)
    {
        _dl.AddProduct(productToAdd);
    }
    public void RemoveProduct(int prodID)
    {
        _dl.RemoveProduct(prodID);
    }
    public void RestockEarthInventory(int prodID, int quantity)
    {
        _dl.RestockEarthInventory(prodID, quantity);
    }
    public List<Order> GetAllEarthOrders()
    {
        return _dl.GetAllEarthOrders();
    }
    public List<Order> GetAllCentauriOrders()
    {
        return _dl.GetAllCentauriOrders();
    }
    public List<Inventory> GetCentauriInventory()
    {
        return _dl.GetCentauriInventory();
    }
    public void RestockCentauriInventory(int prodID, int quantity)
    {
        _dl.RestockCentauriInventory(prodID, quantity);
    }
    public int GetProductID(string productname)
    {
        return _dl.GetProductID(productname);
    }
    public void AddProductToInventory(int prodID, Inventory inventToAdd)
    {
        _dl.AddProductToInventory(prodID, inventToAdd);
    }

}