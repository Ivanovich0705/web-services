using TakeMeHome.API.Shared.Domain.Services.Communication;
using TakeMeHome.API.TakeMeHome.Domain.Models;

namespace TakeMeHome.API.TakeMeHome.Domain.Services.Communication;

public class NotificationsResponse: BaseResponse<Notifications>
{
    public NotificationsResponse(Notifications resource) : base(resource)
    {
    }

    public NotificationsResponse(string message) : base(message)
    {
    }
}