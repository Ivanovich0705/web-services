namespace TakeMeHome.API.TakeMeHome.Resources;

public class ProductResource
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ProductUrl { get; set; }
    public int Price { get; set; }
    public string Store { get; set; }
    public string Currency { get; set; }
    public OrderResource Order { get; set; }
}