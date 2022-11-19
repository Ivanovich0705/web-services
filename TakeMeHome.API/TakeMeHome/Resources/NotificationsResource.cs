using TakeMeHome.API.TakeMeHome.Domain.Models;

namespace TakeMeHome.API.TakeMeHome.Resources;

public class NotificationsResource
{
    public int Id { get; set; }
    
    public string Type { get; set; }
    
    public OrderResource Order { get; set; }
}