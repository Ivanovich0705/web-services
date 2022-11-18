using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TakeMeHome.API.TakeMeHome.Domain.Models;
using TakeMeHome.API.TakeMeHome.Domain.Services;
using TakeMeHome.API.TakeMeHome.Resources;

namespace TakeMeHome.API.TakeMeHome.Controllers;

[Route("/api/v1/[controller]")]

public class OrdersByStatus: ControllerBase
{
    private readonly IOrderService _orderService;
    private readonly IUserService _userService;
    private readonly IOrderStatusService _orderStatusService;
    private readonly IMapper _mapper;
    
    public OrdersByStatus(IOrderService orderService, IUserService userService, IOrderStatusService orderStatusService, IMapper mapper)
    {
        _orderService = orderService;
        _userService = userService;
        _orderStatusService = orderStatusService;
        _mapper = mapper;
    }
    //Devolver las ordenes por estado y user id sin devolver el producto
    [HttpGet]
    [Route("{statusId}/{userId}")]
    public async Task<IEnumerable<OrderResource>> GetOrdersByStatus(int statusId, int userId)
    {
        //var orders = await _orderService.ListByUserIdAsync(userId);
        //orders.Where(o => o.OrderStatus.Id == statusId);
        var orders = await  _orderService.ListByStatusIdAndUserIdAsync(statusId, userId);
        var resources = _mapper.Map<IEnumerable<Order>, IEnumerable<OrderResource>>(orders);
        return resources;
    }

}