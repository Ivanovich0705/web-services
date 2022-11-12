using TakeMeHome.API.TakeMeHome.Domain.Models;

namespace TakeMeHome.API.TakeMeHome.Resources;

public class CommentResource
{
    public int Id { get; set; }
    public int Stars { get; set; }
    public string Content { get; set; }
    public OrderResource Order { get; set; }
}