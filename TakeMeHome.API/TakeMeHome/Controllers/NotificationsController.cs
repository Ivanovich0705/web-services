using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TakeMeHome.API.TakeMeHome.Domain.Models;
using TakeMeHome.API.TakeMeHome.Domain.Services;
using TakeMeHome.API.TakeMeHome.Domain.Services.Communication;
using TakeMeHome.API.TakeMeHome.Resources;

namespace TakeMeHome.API.TakeMeHome.Controllers;

[Route("*/api/v1/[controller]")]
public class NotificationsController: ControllerBase
{
    private readonly INotificationsService _notificationsService;
    private readonly IMapper _mapper;


    public NotificationsController(INotificationsService notificationsService, IMapper mapper)
    {
        _notificationsService = notificationsService;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<IEnumerable<NotificationsResource>> GetAllAsync()
    {
        var notifications = await _notificationsService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Notifications>, IEnumerable<NotificationsResource>>(notifications);
        
        return resources;
    }
}