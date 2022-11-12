namespace TakeMeHome.API.TakeMeHome.Domain.Models;

public class Notifications
{
    public int Id { get; set; }
    
    public string Type { get; set; }
    
    
    //Relationships
    
    
    public int UserId { get; set; }
    
    public User User { get; set; }
}