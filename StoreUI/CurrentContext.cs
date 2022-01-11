namespace UI;

public static class CurrentContext
{
    public static Customer currentCustomer { get; set; }

    public static Storefront currentStore { get; set; }

    public static Order Cart { get; set; }
    public static int OrderId { get; set; }
    public static List<LineItem> lineItems { get; set; }
}