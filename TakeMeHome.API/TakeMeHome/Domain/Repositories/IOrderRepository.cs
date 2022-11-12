using TakeMeHome.API.TakeMeHome.Domain.Models;

namespace TakeMeHome.API.TakeMeHome.Domain.Repositories;

public interface IOrderRepository
{
    Task<IEnumerable<Order>> ListAsync();
    Task AddAsync(Order order);
    Task<Order> FindByIdAsync(int id);
    Task<IEnumerable<Order>> FindByOrderStatusId(int orderStatusId);
    Task<IEnumerable<Order>> FindyByUserId(int userId);
    void Update(Order order);
    void Remove(Order order);
}