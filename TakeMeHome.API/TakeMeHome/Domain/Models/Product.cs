namespace TakeMeHome.API.TakeMeHome.Domain.Models;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ProductUrl { get; set; }
    public int Price { get; set; }
    public string Store { get; set; }
    public string Currency { get; set; }
    
    //Relationships
    public int OrderId { get; set; }
    public Order Order { get; set; }
}