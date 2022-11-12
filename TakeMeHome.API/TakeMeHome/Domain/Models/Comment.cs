namespace TakeMeHome.API.TakeMeHome.Domain.Models;

public class Comment
{
    public int Id { get; set; }
    public int Stars { get; set; }
    public string Content { get; set; }
    
    //Relationships
    public int OrderId { get; set; }
    public Order Order { get; set; }
    
}