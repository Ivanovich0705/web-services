using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TakeMeHome.API.Shared.Extensions;
using TakeMeHome.API.TakeMeHome.Domain.Models;
using TakeMeHome.API.TakeMeHome.Domain.Services;
using TakeMeHome.API.TakeMeHome.Domain.Services.Communication;
using TakeMeHome.API.TakeMeHome.Resources;

namespace TakeMeHome.API.TakeMeHome.Controllers;

[Route("/api/v1/[controller]")]
public class OrdersStatusController: ControllerBase
{
    
    private readonly IOrderStatusService _orderStatusService;
    private readonly  IMapper _mapper;

    public OrdersStatusController(IOrderStatusService orderStatusService, IMapper mapper)
    {
        _orderStatusService = orderStatusService;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<IEnumerable<OrderStatusResource>> GetAllAsync()
    {
        var orderStatus = await _orderStatusService.ListAsync();
        var resources = _mapper.Map<IEnumerable<OrderStatus>, IEnumerable<OrderStatusResource>>(orderStatus);
        return resources;
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var result = await _orderStatusService.FindByIdAsync(id);
        if (!result.Success)
            return BadRequest(result.Message);
        var orderStatusResource = _mapper.Map<OrderStatus, OrderStatusResource>(result.Resource);
        return Ok(orderStatusResource);
    }
    
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] SaveOrderStatusResource resource )
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var orderStatus = _mapper.Map<SaveOrderStatusResource, OrderStatus>(resource);
        var result = await _orderStatusService.SaveAsync(orderStatus);

        if (!result.Success)
            return BadRequest(result.Message);

        var orderStatusResource = _mapper.Map<OrderStatus, OrderStatusResource>(result.Resource);
        return Ok(orderStatusResource);
    }

    
}