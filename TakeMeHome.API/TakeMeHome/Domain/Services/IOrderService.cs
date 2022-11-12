using TakeMeHome.API.TakeMeHome.Domain.Models;
using TakeMeHome.API.TakeMeHome.Domain.Services.Communication;

namespace TakeMeHome.API.TakeMeHome.Domain.Services;

public interface IOrderService
{
    Task<IEnumerable<Order>> ListAsync();
    Task<IEnumerable<Order>> ListByOrderStatusIdAsync(int orderStatusId);
    Task<IEnumerable<Order>> ListByUserIdAsync(int userId);
    Task<OrderResponse> SaveAsync(Order order);
    Task<Order> FindByIdAsync(int orderId);
    Task<OrderResponse> UpdateAsync(int id, Order order);
    Task<OrderResponse> DeleteAsync(int id);
}