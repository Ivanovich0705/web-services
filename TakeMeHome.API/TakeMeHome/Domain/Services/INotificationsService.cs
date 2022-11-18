using TakeMeHome.API.TakeMeHome.Domain.Models;
using TakeMeHome.API.TakeMeHome.Domain.Services.Communication;

namespace TakeMeHome.API.TakeMeHome.Domain.Services;

public interface INotificationsService
{
    Task<IEnumerable<Notifications>> ListAsync();
    Task<IEnumerable<Notifications>> ListByUserIdAsync(int userId);
    Task<IEnumerable<Notifications>> ListByOrderIdAsync(int orderId);
    Task<NotificationsResponse> SaveAsync(Notifications notifications);
    Task<Notifications> FindByIdAsync(int notificationsId);
    
    Task<NotificationsResponse> UpdateAsync(int id, Notifications notifications);
    Task<NotificationsResponse> DeleteAsync(int id);
}