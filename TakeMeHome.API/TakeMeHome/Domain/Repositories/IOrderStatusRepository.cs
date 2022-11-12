using TakeMeHome.API.TakeMeHome.Domain.Models;

namespace TakeMeHome.API.TakeMeHome.Domain.Repositories;

public interface IOrderStatusRepository
{
    Task<IEnumerable<OrderStatus>> ListAsync();
    Task AddAsync(OrderStatus orderStatus);
    Task<OrderStatus> FindByIdAsync(int id);
    void Update(OrderStatus orderStatus);
    void Remove(OrderStatus orderStatus);
}