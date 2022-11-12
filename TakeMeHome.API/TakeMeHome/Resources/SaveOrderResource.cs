using System.ComponentModel.DataAnnotations;

namespace TakeMeHome.API.TakeMeHome.Resources;

public class SaveOrderResource
{
    [Required]
    [MaxLength(30)]
    public string OrderCode { get; set; }
    
    [Required]
    public string OriginCountry { get; set; }
    
    [Required]
    public int UserId { get; set; }
    
    [Required]
    public int ClientId { get; set; }

    [Required]
    public string OrderDestination { get; set; }
    
    [Required]
    public DateTime RequestDate { get; set; }
    
    [Required]
    public DateTime DeadlineDate { get; set; }
    
    public int CurrentProcess { get; set; }
    
    [Required]
    public int OrderStatusId { get; set; }
}