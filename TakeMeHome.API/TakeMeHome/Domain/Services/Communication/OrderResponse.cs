using TakeMeHome.API.Shared.Domain.Services.Communication;
using TakeMeHome.API.TakeMeHome.Domain.Models;

namespace TakeMeHome.API.TakeMeHome.Domain.Services.Communication;

public class OrderResponse : BaseResponse<Order>
{
    public OrderResponse(Order resource) : base(resource)
    {
    }

    public OrderResponse(string message) : base(message)
    {
    }
}