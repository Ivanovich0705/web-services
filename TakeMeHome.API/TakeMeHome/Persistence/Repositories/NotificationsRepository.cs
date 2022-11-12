using Microsoft.EntityFrameworkCore;
using TakeMeHome.API.Shared.Domain.Services.Communication;
using TakeMeHome.API.TakeMeHome.Domain.Models;
using TakeMeHome.API.TakeMeHome.Domain.Repositories;
using TakeMeHome.API.TakeMeHome.Persistence.Contexts;

namespace TakeMeHome.API.TakeMeHome.Persistence.Repositories;

public class NotificationsRepository : BaseRepository  , INotificationsRepository
{
    public NotificationsRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Notifications>> ListAsync()
    {
        return await _context.Notifications.ToListAsync();
    }

    public async Task AddAsync(Notifications notifications)
    {
        await _context.Notifications.AddAsync(notifications);
    }

    public async Task<Notifications> FindIdAsync(int id)
    {
        return await _context.Notifications.FindAsync(id);
    }

    public async Task<IEnumerable<Notifications>> FindByUserId(int userId)
    {
        return await _context.Notifications
            .Where(p => p.User.Id.Equals(userId))
            //.Include(p => p.Order.User)
            .Include(p=>p.User.Id)
            .ToListAsync();
    }
    public void Update(Notifications notifications)
    {
        _context.Notifications.Update(notifications);
    }

    public void Remove(Notifications notifications)
    {
        _context.Notifications.Remove(notifications);
    }
}