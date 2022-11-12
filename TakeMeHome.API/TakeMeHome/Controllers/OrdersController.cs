using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using TakeMeHome.API.Shared.Extensions;
using TakeMeHome.API.TakeMeHome.Domain.Models;
using TakeMeHome.API.TakeMeHome.Domain.Services;
using TakeMeHome.API.TakeMeHome.Resources;

namespace TakeMeHome.API.TakeMeHome.Controllers;

[Route("/api/v1/[controller]")]

//Method Controller for Orders
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;
    private readonly IUserService _userService;
    private readonly IOrderStatusService _orderStatusService;
    private readonly IMapper _mapper;

    public OrdersController(IOrderService orderService, IMapper mapper, IUserService userService, IOrderStatusService orderStatusService)
    {
        _orderService = orderService;
        _mapper = mapper;
        _userService = userService;
        _orderStatusService = orderStatusService;
    }

    [HttpGet]
    public async Task<IEnumerable<OrderResource>> GetAllAsync()
    {
        var orders = await _orderService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Order>, IEnumerable<OrderResource>>(orders);
        return resources;
    }
    
    [HttpGet]
    [Route("/status/{status_id}")]
    public async Task<IEnumerable<OrderResource>> GetByStatusIdAsync(int status_id)
    {
        var orders = await _orderService.ListByOrderStatusIdAsync(status_id);
        var resources = _mapper.Map<IEnumerable<Order>, IEnumerable<OrderResource>>(orders);
        return resources;
    }
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] SaveOrderResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
        
         var existingClient = await _userService.FindByIdAsync(resource.UserId);
         if (existingClient == null)
                return BadRequest("Client doesnt exists");
         
         var existingTourist = await _userService.FindByIdAsync(resource.ClientId);
            if (existingTourist == null)
                    return BadRequest("Tourist doesnt exists");
         
        var exisingStatus = await _orderStatusService.FindByIdAsync(resource.OrderStatusId);
        if (exisingStatus == null)
            return BadRequest("Order Status doesnt exists");
            
        var order = _mapper.Map<SaveOrderResource, Order>(resource);
        var result = await _orderService.SaveAsync(order);

        if (!result.Success)
            return BadRequest(result.Message);

        var orderResource = _mapper.Map<Order, OrderResource>(result.Resource);
        return Ok(orderResource);
    }
    
    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateAsync(int id, [FromBody] JsonPatchDocument<Order> resource)
    {
        var order = await _orderService.FindByIdAsync(id);
        
        if (order == null)
        {
            return NotFound();
        }
        
        resource.ApplyTo(order);
        
        
        var result = await _orderService.UpdateAsync(id, order);
        
        if (!result.Success)
            return BadRequest(result.Message);
        
        var orderResource = _mapper.Map<Order, OrderResource>(result.Resource);
        return Ok(order);
    }
}