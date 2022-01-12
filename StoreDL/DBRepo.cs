using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Models;
using System.Data;
using Serilog;
namespace StoreDL;


public class DBRepo : IRepo
{
    private string _connectionString;
    public DBRepo(string connectionString)
    {
        _connectionString = connectionString;
    }
    /// <summary>
    /// Adds A new Customer to the database
    /// </summary>
    /// <param name="newCustomer">The new customer signing up</param>
    public void AddCustomer(Customer newCustomer)
    {
        int CID = Customer.CId;
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            string sqlCmd = "INSERT INTO Customer (CustomerId, UserName, PassWord) VALUES (@p1, @p2, @p3)";
            using(SqlCommand cmd = new SqlCommand(sqlCmd, connection))
            {
                cmd.Parameters.Add(new SqlParameter("@p1", CID));
                cmd.Parameters.Add(new SqlParameter("@p2", newCustomer.UserName));
                cmd.Parameters.Add(new SqlParameter("@p3", newCustomer.Password));
                cmd.ExecuteNonQuery();
            }
            connection.Close();
        }
    }
    public void AddStore(Storefront storetoAdd)
    {
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            string sqlCmd = "INSERT INTO StoreFront (StoreId, Name, Address) VALUES (@p1, @p2, @p3)";
            using(SqlCommand cmd = new SqlCommand(sqlCmd, connection))
            {
                cmd.Parameters.Add(new SqlParameter("@p1", storetoAdd.StoreID));
                cmd.Parameters.Add(new SqlParameter("@p2", storetoAdd.Name));
                cmd.Parameters.Add(new SqlParameter("@p3", storetoAdd.Address));
                cmd.ExecuteNonQuery();
            }
            connection.Close();
        }
    }
    /// <summary>
    /// Gets a list of every customer that has signed up
    /// </summary>
    /// <returns>a list of all customers</returns>
    public List<Customer> GetAllCustomers()
    {
        int CID = Customer.CId;
        List<Customer> allCustomers = new List<Customer>();
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            string queryTxt = "SELECT * FROM Customer";
            using(SqlCommand cmd = new SqlCommand(queryTxt, connection))
            {
                using(SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Customer cust = new Customer();
                        CID = reader.GetInt32(0);
                        cust.UserName = reader.GetString(1);
                        cust.Password = reader.GetString(2);

                        allCustomers.Add(cust);
                    }
                }
            }
            connection.Close();
        }
        return allCustomers;
    }
    public List<Storefront> GetAllStores()
    {
        List<Storefront> allStores = new List<Storefront>();
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            string queryTxt = "SELECT * FROM StoreFront";
            using(SqlCommand cmd = new SqlCommand(queryTxt, connection))
            {
                using(SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Storefront store = new Storefront();
                        store.StoreID = reader.GetInt32(0);
                        store.Name = reader.GetString(1);
                        store.Address = reader.GetString(2);

                        allStores.Add(store);
                    }
                }
            }
            connection.Close();
        }
        return allStores;
    }
    /// <summary>
    /// Finds all the products in the database that are associated with a certain StoreId
    /// </summary>
    /// <returns>a list of all the products in the Earth Store</returns>
    public List<Product> GetAllEarthProducts()
    {
        List<Product> allProducts = new List<Product>();
        using SqlConnection connection = new SqlConnection(_connectionString);
        string prodSelect = "SELECT p.ProductID, p.Name, p.Description, p.Price, i.StoreId, i.Quantity\nFROM Product p\nINNER JOIN Inventory i ON p.ProductID = i.ProductId\n WHERE i.StoreId = 1\nORDER BY p.ProductID";
        DataSet ProdSet = new DataSet();
        using SqlDataAdapter prodAdapter = new SqlDataAdapter(prodSelect, connection);
        prodAdapter.Fill(ProdSet, "Product");
        DataTable ?ProductTable = ProdSet.Tables["Product"];
        foreach(DataRow row in ProductTable.Rows)
        {
            Product prod = new Product();
            prod.ProductID = (int) row["ProductID"];
            prod.ProductName = row["Name"].ToString();
            prod.Description = row["Description"].ToString();
            prod.Price = (int) row["Price"];
            allProducts.Add(prod);
        }
        return allProducts;
    }
    public List<Product> GetAllCentauriProducts()
    {
        List<Product> allProducts = new List<Product>();
        using SqlConnection connection = new SqlConnection(_connectionString);
        string prodSelect = "SELECT p.ProductID, p.Name, p.Description, p.Price, i.StoreId, i.Quantity\nFROM Product p\nINNER JOIN Inventory i ON p.ProductID = i.ProductId\n WHERE i.StoreId = 2\nORDER BY p.ProductID";
        DataSet ProdSet = new DataSet();
        using SqlDataAdapter prodAdapter = new SqlDataAdapter(prodSelect, connection);
        prodAdapter.Fill(ProdSet, "Product");
        DataTable ?ProductTable = ProdSet.Tables["Product"];
        foreach(DataRow row in ProductTable.Rows)
        {
            Product prod = new Product();
            prod.ProductID = (int) row["ProductID"];
            prod.ProductName = row["Name"].ToString();
            prod.Description = row["Description"].ToString();
            prod.Price = (int) row["Price"];
            allProducts.Add(prod);
        }
        return allProducts;
    }
    /// <summary>
    /// Finds all the orders ever placed by a single person
    /// </summary>
    /// <param name="CID">The CustomerID of the current user</param>
    /// <returns>all orders placed by that user</returns>
    public List<Order> GetAllOrders(int CID)
    {
        CID = Customer.CId;
        List<Order> allOrders = new List<Order>();
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            string queryTxt = $"SELECT * FROM Orders WHERE CustomerId = {CID}";
            using(SqlCommand cmd = new SqlCommand(queryTxt, connection))
            {
                using(SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Order order = new Order();
                        order.OrderNumber = reader.GetInt32(0);
                        CID = reader.GetInt32(1);
                        order.StoreId = reader.GetInt32(2);
                        order.Total = reader.GetInt32(3);
                        order.OrderDate = reader.GetDateTime(4);

                        allOrders.Add(order);
                    }
                }
            }
            connection.Close();
        }
        return allOrders;
    }
    /// <summary>
    /// Finds the randomly generated customerID for the currently logged in user
    /// </summary>
    /// <param name="username">searches based on the username</param>
    /// <returns>an integer customerID</returns>
    public int GetCustomerID(string username)
    {
        int CID = Customer.CId;
        Customer currentCustomer = new Customer();
        using SqlConnection connection = new SqlConnection(_connectionString);
        {
            connection.Open();
            string queryTxt = $"SELECT CustomerId FROM Customer WHERE UserName = '{username}'";
            using(SqlCommand cmd = new SqlCommand(queryTxt, connection))
            {
                using(SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        CID = reader.GetInt32(0);
                    }
                }
            }
            connection.Close();
        }
        return CID;
    }
    public int GetProductID(string productname)
    {
        int prodID = 0;
        Product currProd = new Product();
        using SqlConnection connection = new SqlConnection(_connectionString);
        {
            connection.Open();
            string queryTxt = $"SELECT ProductID FROM Product WHERE Name = '{productname}'";
            using(SqlCommand cmd = new SqlCommand(queryTxt, connection))
            {
                using(SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        prodID = reader.GetInt32(0);
                    }
                }
            }
            connection.Close();
        }
        return prodID;
    }
    /// <summary>
    /// When a product is selected to order, it is added as a line, each line item is added to an order
    /// </summary>
    /// <param name="newLI">the currently selected product</param>
    /// <param name="orderID">The order number that this instance of purchasing belongs to</param>
    public void AddLineItem(LineItem newLI, int orderID)
    {
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            string sqlCmd = "INSERT INTO LineItem (Product, OrderId, Quantity) VALUES (@p1, @p2, @p3)";
            using(SqlCommand cmd = new SqlCommand(sqlCmd, connection))
            {
                cmd.Parameters.Add(new SqlParameter("@p1", newLI.ProductID));
                cmd.Parameters.Add(new SqlParameter("@p2", orderID));
                cmd.Parameters.Add(new SqlParameter("@p3", newLI.Quantity));
                cmd.ExecuteNonQuery();
            }
            connection.Close();
            Log.Information("LineItem added {ProductID}{OrderID}{quantity}", newLI.ProductID,newLI.OrderId,newLI.Quantity);
        }
    }
    /// <summary>
    /// Adds a completed order to the database
    /// </summary>
    /// <param name="orderToAdd">The current order</param>
    public void AddOrder(Order orderToAdd)
    {
        DataSet OrderSet = new DataSet();
        string selectCmd = "SELECT * FROM Orders";
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            using(SqlDataAdapter dataAdapter = new SqlDataAdapter(selectCmd, connection))
            {
                dataAdapter.Fill(OrderSet, "Orders");
                DataTable ?orderTable = OrderSet.Tables["Orders"];
                DataRow newRow = orderTable.NewRow();
                orderToAdd.ToDataRow(ref newRow);

                orderTable.Rows.Add(newRow);
                string insertCmd = $"INSERT INTO Orders (OrderId, CustomerId, StoreId, Total, OrderDate) VALUES ('{orderToAdd.OrderNumber}', '{orderToAdd.CustomerId}', '{orderToAdd.StoreId}', '{orderToAdd.Total}', '{orderToAdd.OrderDate}')";

                SqlCommandBuilder cmdBuilder = new SqlCommandBuilder(dataAdapter);
                dataAdapter.InsertCommand = cmdBuilder.GetInsertCommand();
                dataAdapter.Update(orderTable);
                // dataAdapter.Insert(orderTable);
                Log.Information("Order added {OrderId}{CustomerId}{StoreId}{Total}{OrderDate}", orderToAdd.OrderNumber,orderToAdd.CustomerId,orderToAdd.StoreId,orderToAdd.Total,orderToAdd.OrderDate);
            }
        }
    }
    //     public void AddOrder(Order orderToAdd)
    // {
        
    //     using(SqlConnection connection = new SqlConnection(_connectionString))
    //     {
    //         connection.Open();
    //         string sqlCmd = "INSERT INTO Orders (OrderId, CustomerId, StoreId, Total, OrderDate) VALUES (@p1, @p2, @p3, @p4, @p5)";
    //         using(SqlCommand cmd = new SqlCommand(sqlCmd, connection))
    //         {
    //             cmd.Parameters.Add(new SqlParameter("@p1", orderToAdd.OrderNumber));
    //             cmd.Parameters.Add(new SqlParameter("@p2", orderToAdd.CustomerId));
    //             cmd.Parameters.Add(new SqlParameter("@p3", orderToAdd.StoreId));
    //             cmd.Parameters.Add(new SqlParameter("@p4", orderToAdd.Total));
    //             cmd.Parameters.Add(new SqlParameter("@p5", orderToAdd.OrderDate));
    //             cmd.ExecuteNonQuery();
    //         }
    //         connection.Close();
    //     }
    // }
    public List<Inventory> GetEarthInventory()
    {
        List<Inventory> earthInventory = new List<Inventory>();
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            string queryTxt = "SELECT * FROM Inventory WHERE StoreId = 1";
            using(SqlCommand cmd = new SqlCommand(queryTxt, connection))
            {
                using(SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Inventory earth = new Inventory();
                        earth.InventoryID = reader.GetInt32(0);
                        earth.StoreId = reader.GetInt32(1);
                        earth.ProductID = reader.GetInt32(2);
                        earth.Quantity = reader.GetInt32(3);

                        earthInventory.Add(earth);
                    }
                }
            }
            connection.Close();
        }
        return earthInventory;
    }
    
    public List<Inventory> GetCentauriInventory()
    {
        List<Inventory> centauriInventory = new List<Inventory>();
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            string queryTxt = "SELECT * FROM Inventory WHERE StoreId = 2";
            using(SqlCommand cmd = new SqlCommand(queryTxt, connection))
            {
                using(SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Inventory centauri = new Inventory();
                        centauri.InventoryID = reader.GetInt32(0);
                        centauri.StoreId = reader.GetInt32(1);
                        centauri.ProductID = reader.GetInt32(2);
                        centauri.Quantity = reader.GetInt32(3);
                        centauriInventory.Add(centauri);
                    }
                }
            }
            connection.Close();
        }
        return centauriInventory;
    }
    /// <summary>
    /// Adds a new product to the database
    /// </summary>
    /// <param name="productToAdd">the new product you want to add</param>
    public void AddProduct(Product productToAdd)
    {
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            string sqlCmd = "INSERT INTO Product (Name, Description, Price) VALUES (@p1, @p2, @p3)";
            using(SqlCommand cmd = new SqlCommand(sqlCmd, connection))
            {
                cmd.Parameters.Add(new SqlParameter("@p1", productToAdd.ProductName));
                cmd.Parameters.Add(new SqlParameter("@p2", productToAdd.Description));
                cmd.Parameters.Add(new SqlParameter("@p3", productToAdd.Price));
                cmd.ExecuteNonQuery();
            }
            connection.Close();
            Log.Information("Product added {name}{description}{price}", productToAdd.ProductName,productToAdd.Description,productToAdd.Price);
        }
    }
    public void AddProductToInventory(int prodID, Inventory inventToAdd)
    {
        Inventory invent = new Inventory();
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            string sqlCmd = "INSERT INTO Inventory (StoreId, ProductId, Quantity) VALUES (@p1, @p2, @p3)";
            using(SqlCommand cmd = new SqlCommand(sqlCmd, connection))
            {
                cmd.Parameters.Add(new SqlParameter("@p1", inventToAdd.StoreId));
                cmd.Parameters.Add(new SqlParameter("@p2", prodID));
                cmd.Parameters.Add(new SqlParameter("@p3", inventToAdd.Quantity));
                cmd.ExecuteNonQuery();
            }
            connection.Close();
            Log.Information("Inventory added {StoreId}{ProductId}{Quantity}", inventToAdd.StoreId,prodID,inventToAdd.Quantity);
        }
    }
    public void RemoveProduct(int prodID)
    {
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            string sqlCmd = "DELETE FROM Product WHERE ProductID = @p1";
            using(SqlCommand cmd = new SqlCommand(sqlCmd, connection))
            {
                cmd.Parameters.Add(new SqlParameter("@p1", prodID));
                
                cmd.ExecuteNonQuery();
            }
            connection.Close();
        }
    }
    public void RestockEarthInventory(int prodID, int quantity)
    {
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            string sqlCmd = "UPDATE Inventory SET Quantity = @p0 WHERE ProductId = @p1";
            using(SqlCommand cmd = new SqlCommand(sqlCmd, connection))
            {
                cmd.Parameters.AddWithValue("@p0", quantity);
                cmd.Parameters.AddWithValue("@p1", prodID);
                cmd.ExecuteNonQuery();
            }
            connection.Close();
        }
    }
        public void RestockCentauriInventory(int prodID, int quantity)
    {
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            string sqlCmd = "UPDATE Inventory SET Quantity = @p0 WHERE ProductId = @p1";
            using(SqlCommand cmd = new SqlCommand(sqlCmd, connection))
            {
                cmd.Parameters.AddWithValue("@p0", quantity);
                cmd.Parameters.AddWithValue("@p1", prodID);
                cmd.ExecuteNonQuery();
            }
            connection.Close();
        }
    }
    /// <summary>
    /// Finds all orders ever placed at a single store
    /// </summary>
    /// <returns>list of all orders, ordered by total price</returns>
    public List<Order> GetAllEarthOrders()
    {
        List<Order> allOrders = new List<Order>();
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            string queryTxt = $"SELECT * FROM Orders WHERE StoreId = 1 ORDER BY Total DESC";
            using(SqlCommand cmd = new SqlCommand(queryTxt, connection))
            {
                using(SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Order order = new Order();
                        order.OrderNumber = reader.GetInt32(0);
                        order.CustomerId = reader.GetInt32(1);
                        order.Total = reader.GetInt32(3);
                        order.OrderDate = reader.GetDateTime(4);

                        allOrders.Add(order);
                    }
                }
            }
            connection.Close();
        }
        return allOrders;
    }
    public List<Order> GetAllCentauriOrders()
    {
        List<Order> allOrders = new List<Order>();
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            string queryTxt = $"SELECT * FROM Orders WHERE StoreId = 2 ORDER BY Total DESC";
            using(SqlCommand cmd = new SqlCommand(queryTxt, connection))
            {
                using(SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Order order = new Order();
                        order.OrderNumber = reader.GetInt32(0);
                        order.CustomerId = reader.GetInt32(1);
                        order.Total = reader.GetInt32(3);
                        order.OrderDate = reader.GetDateTime(4);

                        allOrders.Add(order);
                    }
                }
            }
            connection.Close();
        }
        return allOrders;
    }

}