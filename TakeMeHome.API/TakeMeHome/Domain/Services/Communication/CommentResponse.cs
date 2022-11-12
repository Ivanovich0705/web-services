using TakeMeHome.API.Shared.Domain.Services.Communication;
using TakeMeHome.API.TakeMeHome.Domain.Models;

namespace TakeMeHome.API.TakeMeHome.Domain.Services.Communication;

public class CommentResponse : BaseResponse<Comment>
{
    public CommentResponse(Comment resource) : base(resource)
    {
    }

    public CommentResponse(string message) : base(message)
    {
    }
}