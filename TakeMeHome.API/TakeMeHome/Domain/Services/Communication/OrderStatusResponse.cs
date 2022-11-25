using TakeMeHome.API.Shared.Domain.Services.Communication;
using TakeMeHome.API.TakeMeHome.Domain.Models;

namespace TakeMeHome.API.TakeMeHome.Domain.Services.Communication;

public class OrderStatusResponse : BaseResponse<OrderStatus>
{
    public OrderStatusResponse(OrderStatus resource) : base(resource)
    {
    }

    public OrderStatusResponse(string message) : base(message)
    {
    }
}