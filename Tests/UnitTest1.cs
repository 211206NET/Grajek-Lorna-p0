using Xunit;
using Models;
using System.Collections.Generic;
namespace Tests;

public class StoreModelsTest
{
    [Fact]
    public void AddStore(){

    Storefront TestStore = new Storefront();
    Assert.NotNull(TestStore);

    }
    [Fact]
    public void AddCustomer(){

    Customer TestUser = new Customer();
    Assert.NotNull(TestUser);

    }

    [Fact]
    public void CustomerAccountShouldSetValidData()
    {
        //Arrange
        Customer TestCustomer = new Customer();
            string Name = "Testname";
            string Password = "1231";
            int CId = 4121312;
            
        //Act
        TestCustomer.Name = Name;
        TestCustomer.Password = Password;
        TestCustomer.CId = CId;

        //Assert
        Assert.Equal(Name, TestCustomer.Name);
        Assert.Equal(Password, TestCustomer.Password);
        Assert.Equal(CId, TestCustomer.CId);

    }
    [Fact]
    public void StoreOrderShouldBeAbleToSet()
    {
        //Arrange
        Storefront testStore = new Storefront();
        List<Order> testOrders = new List<Order>();

        testStore.Orders = testOrders;

        //Assert
        Assert.NotNull(testStore.Orders);
        Assert.Equal(0, testStore.Orders.Count);
    }

    

    


    
}