using TakeMeHome.API.TakeMeHome.Domain.Models;

namespace TakeMeHome.API.TakeMeHome.Domain.Repositories;

public interface INotificationsRepository
{
    Task<IEnumerable<Notifications>> ListAsync();
    Task AddAsync(Notifications notifications);
    Task<Notifications> FindIdAsync(int id);
    Task<IEnumerable<Notifications>> FindByUserId(int userId);
    void Update(Notifications notifications);
    void Remove(Notifications notifications);
}