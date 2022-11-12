using TakeMeHome.API.TakeMeHome.Domain.Models;
using TakeMeHome.API.TakeMeHome.Domain.Repositories;
using TakeMeHome.API.TakeMeHome.Domain.Services;
using TakeMeHome.API.TakeMeHome.Domain.Services.Communication;

namespace TakeMeHome.API.TakeMeHome.Services;

public class NotificationsService : INotificationsService
{
    private readonly INotificationsRepository _notificationsRepository;

    private readonly IUnitOfWork _unitOfWork;

    public NotificationsService(INotificationsRepository notificationsRepository, IUnitOfWork unitOfWork)
    {
        _notificationsRepository = notificationsRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Notifications>> ListAsync()
    {
        return await _notificationsRepository.ListAsync();
    }
    public async Task<IEnumerable<Notifications>> ListByUserIdAsync(int userId)
    {
        return await _notificationsRepository.FindByUserId(userId);
    }
    public async Task<NotificationsResponse> SaveAsync(Notifications notifications)
    {
        try
        {
            await _notificationsRepository.AddAsync(notifications);
            await _unitOfWork.CompleteAsync();

            return new NotificationsResponse(notifications);
        }
        catch (Exception e)
        {
            return new NotificationsResponse($"An error occurred while saving the notifications: {e.Message}");
        }
    }

    public async Task<NotificationsResponse> UpdateAsync(int id, Notifications notifications)
    {
        var existingNotifications = await _notificationsRepository.FindIdAsync(id);

        if (existingNotifications == null)
            return new NotificationsResponse("Notification not found.");

        existingNotifications.Type = notifications.Type;

        try
        {
            _notificationsRepository.Update(existingNotifications);
            await _unitOfWork.CompleteAsync();

            return new NotificationsResponse(existingNotifications);
        }
        catch (Exception e)
        {
            return new NotificationsResponse($"An error occurred while updating the notifications: {e.Message}");
        }
    }

    public async Task<NotificationsResponse> DeleteAsync(int id)
    {
        var existingNotifications = await _notificationsRepository.FindIdAsync(id);
        
        if(existingNotifications == null)
            return new NotificationsResponse("Notifications not found.");
        try
        {
            _notificationsRepository.Remove(existingNotifications);
            await _unitOfWork.CompleteAsync();
            
            return new NotificationsResponse(existingNotifications);
        }
        catch (Exception e)
        {
            return new NotificationsResponse($"An Notifications occurred while deleting the comment: {e.Message}");
        }
    }
}