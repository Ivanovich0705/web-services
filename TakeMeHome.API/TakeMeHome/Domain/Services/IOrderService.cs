using TakeMeHome.API.TakeMeHome.Domain.Models;
using TakeMeHome.API.TakeMeHome.Domain.Services.Communication;

namespace TakeMeHome.API.TakeMeHome.Domain.Services;

public interface IOrderService
{
    Task<IEnumerable<Order>> ListAsync();
    Task<IEnumerable<Order>> ListByOrderStatusIdAsync(int orderStatusId);
    Task<IEnumerable<Order>> ListByOrderStatusIdAndUserId(int orderStatusId, int userId);
    Task<IEnumerable<Order>> ListByUserIdAsync(int userId);
    Task<OrderResponse> SaveAsync(Order order);
    Task<Order> FindByIdAsync(int orderId);
    Task<Order> FindByOrderCodeAndUserIdAsync(string orderCode, int userId);
    Task<IEnumerable<Order>> ListByStatusIdAndUserIdAsync(int orderStatusId, int userId);
    Task<OrderResponse> UpdateAsync(int id, Order order);
    Task<OrderResponse> DeleteAsync(int id);
}