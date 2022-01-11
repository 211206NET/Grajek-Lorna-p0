namespace UI;

public static class CurrentContext
{
    public static Customer currentCustomer { get; set; }

    public static Storefront currentStore { get; set; }

    public static Order Cart { get; set; }
    public static int OrderId { get; set; }
    public static List<LineItem> lineItems { get; set; }
    public static decimal CalculateTotal() 
    {
        decimal total1 = 0;
        if(lineItems?.Count > 0)
        {
            int Count = lineItems.Count;
            LineItem lineitem = lineItems.Last();
            total1 += lineitem.Item.Price * lineitem.Quantity;
        }
        return total1;
    }
}