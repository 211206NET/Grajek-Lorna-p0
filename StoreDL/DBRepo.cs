namespace StoreDL;

using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Models;
using System.Data;

public class DBRepo : IRepo
{
    private string _connectionString;
    public DBRepo(string connectionString)
    {
        _connectionString = connectionString;
    }
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
    public List<Product> GetAllEarthProducts()
    {
        List<Product> allProducts = new List<Product>();
        using SqlConnection connection = new SqlConnection(_connectionString);
        string prodSelect = "SELECT p.ProductID, p.Name, p.Description, p.Price, i.StoreId, i.Quantity\nFROM Product p\nINNER JOIN Inventory i ON p.ProductID = i.ProductId\n WHERE i.StoreId = 1\nORDER BY p.ProductID";
        DataSet ProdSet = new DataSet();
        using SqlDataAdapter prodAdapter = new SqlDataAdapter(prodSelect, connection);
        prodAdapter.Fill(ProdSet, "Product");
        DataTable? ProductTable = ProdSet.Tables["Product"];
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
        DataTable? ProductTable = ProdSet.Tables["Product"];
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
    public void AddLineItem(LineItem newLI)
    {
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            string sqlCmd = "INSERT INTO LineItem (Product, OrderId, Quantity) VALUES (@p1, @p2, @p3)";
            using(SqlCommand cmd = new SqlCommand(sqlCmd, connection))
            {
                cmd.Parameters.Add(new SqlParameter("@p1", newLI.ProductID));
                cmd.Parameters.Add(new SqlParameter("@p2", newLI.OrderId));
                cmd.Parameters.Add(new SqlParameter("@p3", newLI.Quantity));
                cmd.ExecuteNonQuery();
            }
            connection.Close();
        }
    }
    public void AddOrder(Order orderToAdd)
    {
        DataSet OrderSet = new DataSet();
        string selectCmd = "SELECT * FROM Orders WHERE OrderId = -1";
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            using(SqlDataAdapter dataAdapter = new SqlDataAdapter(selectCmd, connection))
            {
                dataAdapter.Fill(OrderSet, "Orders");
                DataTable orderTable = OrderSet.Tables["Orders"];
                DataRow newRow = orderTable.NewRow();
                orderToAdd.ToDataRow(ref newRow);

                orderTable.Rows.Add(newRow);
                string insertCmd = $"INSERT INTO Orders (OrderId, CustomerId, StoreId, Total, OrderDate) VALUES ('{orderToAdd.OrderNumber}', '{orderToAdd.CustomerId}', '{orderToAdd.StoreId}', '{orderToAdd.Total}', '{orderToAdd.OrderDate}')";

                SqlCommandBuilder cmdBuilder = new SqlCommandBuilder(dataAdapter);
                dataAdapter.InsertCommand = cmdBuilder.GetInsertCommand();
                dataAdapter.Update(orderTable);
            }
        }
    }
}