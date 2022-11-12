using TakeMeHome.API.TakeMeHome.Domain.Models;

namespace TakeMeHome.API.TakeMeHome.Resources;

public class OrderResource
{
    public int Id { get; set; }
    public string OrderCode { get; set; }
    public int UserId { get; set; }
    public  UserResource User { get; set; }
    public int ClientId { get; set; }
    public UserResource Client { get; set; }
    public string OriginCountry { get; set; }
    public string OrderDestination { get; set; }
    public DateTime RequestDate { get; set; }
    public DateTime DeadlineDate { get; set; }
    public int CurrentProcess { get; set; }
    public OrderStatusResource OrderStatus { get; set; }
}