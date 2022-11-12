using TakeMeHome.API.TakeMeHome.Domain.Models;
using TakeMeHome.API.TakeMeHome.Domain.Services.Communication;

namespace TakeMeHome.API.TakeMeHome.Domain.Services;

public interface IOrderStatusService
{
    Task<IEnumerable<OrderStatus>> ListAsync();
    Task<OrderStatusResponse> SaveAsync(OrderStatus orderStatus);
    Task<OrderStatusResponse> FindByIdAsync(int id);
    Task<OrderStatusResponse> UpdateAsync(int id, OrderStatus orderStatus);
    Task<OrderStatusResponse> DeleteAsync(int id);
}