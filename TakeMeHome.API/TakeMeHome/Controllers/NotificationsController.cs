using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using TakeMeHome.API.Shared.Extensions;
using TakeMeHome.API.TakeMeHome.Domain.Models;
using TakeMeHome.API.TakeMeHome.Domain.Services;
using TakeMeHome.API.TakeMeHome.Domain.Services.Communication;
using TakeMeHome.API.TakeMeHome.Resources;

namespace TakeMeHome.API.TakeMeHome.Controllers;

[Route("/api/v1/[controller]")]
public class NotificationsController: ControllerBase
{
    private readonly INotificationsService _notificationsService;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;


    public NotificationsController(INotificationsService notificationsService, IMapper mapper, IUserService userService)
    {
        _notificationsService = notificationsService;
        _mapper = mapper;
        _userService = userService;
    }
    
    [HttpGet]
    public async Task<IEnumerable<NotificationsResource>> GetAllAsync()
    {
        var notifications = await _notificationsService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Notifications>, IEnumerable<NotificationsResource>>(notifications);
        
        return resources;
    }
        
    [HttpGet]
    [Route("{user_id}")]
    public async Task<IEnumerable<NotificationsResource>> GetByUserIdAsync(int user_id)
    {
        var notifications = await _notificationsService.ListByUserIdAsync(user_id);
        var resources = _mapper.Map<IEnumerable<Notifications>, IEnumerable<NotificationsResource>>(notifications);
        return resources;
    }
    

    //[HttpGet]
    //[Route("{order_id}")]
    //public async Task<IEnumerable<NotificationsResource>> GetByOrderIdAsync(int order_id)
    //{
    //    var notifications = await _notificationsService.ListByUserIdAsync(order_id);
    //    var resources = _mapper.Map<IEnumerable<Notifications>, IEnumerable<NotificationsResource>>(notifications);
    //    return resources;
    //}

    
    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateAsync(int id, [FromBody] JsonPatchDocument<Notifications> resource)
    {
        var notifications = await _notificationsService.FindByIdAsync(id);
        
        if (notifications == null)
        {
            return NotFound();
        }
        
        resource.ApplyTo(notifications);
        
        
        var result = await _notificationsService.UpdateAsync(id, notifications);
        
        if (!result.Success)
            return BadRequest(result.Message);
        
        var notificationsResource = _mapper.Map<Notifications, NotificationsResource>(result.Resource);
        return Ok(notifications);
    }
    
    
    
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] SaveNotificationsResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
        
        var notifications = _mapper.Map<SaveNotificationsResource, Notifications>(resource);
        var result = await _notificationsService.SaveAsync(notifications);
        
        if (!result.Success)
            return BadRequest(result.Message);
        
        var notificationsResource = _mapper.Map<Notifications, NotificationsResource>(result.Resource);
        return Ok(notificationsResource);
    }
    
    [HttpDelete]
    [Route("{notifications_id}")]
    public async Task<IActionResult> DeleteAsync(int notifications_id)
    {
        var result = await _notificationsService.DeleteAsync(notifications_id);
        
        if (!result.Success)
            return BadRequest(result.Message);
        
        var notificationsResource = _mapper.Map<Notifications, NotificationsResource>(result.Resource);
        return Ok(notificationsResource);
    }
    
    
 
    
    [HttpGet("order/{id}")]
    public async Task<IActionResult> GetByOrderIdAsync(int id)
    {
        var notifications = await _notificationsService.FindByOrderIdAsync(id);
        var resources = _mapper.Map<Notifications, NotificationsResource>(notifications);
        return Ok(resources);
    }
    
    [HttpGet]
    [Route("order/user/{id}")]
    public async Task<IEnumerable<NotificationsResource>> GetByOrderAndUserIdAsync(int id)
    {
        var notifications = await _notificationsService.ListByOrderAndUserId(id);
        var resources = _mapper.Map<IEnumerable<Notifications>, IEnumerable<NotificationsResource>>(notifications);
        return resources;
    }
    
    //[HttpGet]
    //[Route("orderCode/{orderCode}/user/{userId}")]
    //public async Task<OrderResource> GetByOrderCodeAndUserIdAsync(string orderCode, int userId)
    //{
    //    var order = await _orderService.FindByOrderCodeAndUserIdAsync(orderCode, userId);
    //    var resource = _mapper.Map<Order, OrderResource>(order);
    //    return resource;
    //}
    
}