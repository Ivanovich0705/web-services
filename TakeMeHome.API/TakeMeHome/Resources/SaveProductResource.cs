namespace TakeMeHome.API.TakeMeHome.Resources;

public class SaveProductResource
{
    public string Name { get; set; }
    public string ProductUrl { get; set; }
    public int Price { get; set; }
    public string Store { get; set; }
    public string Currency { get; set; }
    public int OrderId { get; set; }
}