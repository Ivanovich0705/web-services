using Microsoft.EntityFrameworkCore;
using TakeMeHome.API.TakeMeHome.Domain.Models;
using TakeMeHome.API.TakeMeHome.Domain.Repositories;
using TakeMeHome.API.TakeMeHome.Persistence.Contexts;

namespace TakeMeHome.API.TakeMeHome.Persistence.Repositories;

public class OrderStatusRepository : BaseRepository, IOrderStatusRepository
{
    public OrderStatusRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<OrderStatus>> ListAsync()
    {
        return await _context.OrderStatus.ToListAsync();
    }

    public async Task AddAsync(OrderStatus orderStatus)
    {
        await _context.OrderStatus.AddAsync(orderStatus);
    }

    public async Task<OrderStatus> FindByIdAsync(int id)
    {
        return await _context.OrderStatus.FindAsync(id);
    }

    public void Update(OrderStatus orderStatus)
    {
        _context.OrderStatus.Update(orderStatus);
    }

    public void Remove(OrderStatus orderStatus)
    {
        _context.OrderStatus.Remove(orderStatus);
    }
}