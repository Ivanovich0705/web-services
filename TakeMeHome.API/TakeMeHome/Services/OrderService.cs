using TakeMeHome.API.TakeMeHome.Domain.Models;
using TakeMeHome.API.TakeMeHome.Domain.Repositories;
using TakeMeHome.API.TakeMeHome.Domain.Services;
using TakeMeHome.API.TakeMeHome.Domain.Services.Communication;

namespace TakeMeHome.API.TakeMeHome.Services;

public class OrderService : IOrderService
{
    
    private readonly IOrderRepository _orderRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IOrderStatusRepository _orderStatusRepository;
    public OrderService(IOrderRepository orderRepository, IUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository;
        _unitOfWork = unitOfWork;
    }

    
    
    public async Task<IEnumerable<Order>> ListAsync()
    {
        return await _orderRepository.ListAsync();
    }

    public async Task<IEnumerable<Order>> ListByOrderStatusIdAsync(int orderStatusId)
    {
        return await _orderRepository.FindByOrderStatusId(orderStatusId);
    }

    public async Task<IEnumerable<Order>> ListByUserIdAsync(int userId)
    {
        return await _orderRepository.FindyByUserId(userId);
    }

    public async Task<IEnumerable<Order>> ListByStatusIdAndUserIdAsync(int orderStatusId, int userId)
    {
        return await _orderRepository.FindByStatusIdAndUserId(orderStatusId, userId);
    }
    public async Task<OrderResponse> SaveAsync(Order order)
    {
        //Validate OrderStatusId
        //var existingOrderStatus = await _orderStatusRepository.FindByIdAsync(order.StatusId);
        
        //if (existingOrderStatus == null)
        //{
        //    return new OrderResponse("Invalid OrderStatusId.");
        //}
        //else
        {
            try
            {
                await _orderRepository.AddAsync(order);
                await _unitOfWork.CompleteAsync();

                return new OrderResponse(order);
            }
            catch (Exception e)
            {
                return new OrderResponse($"An error occurred while saving the order: {e.Message}");
            }
        }
    }

    public async Task<Order> FindByIdAsync(int orderId)
    {
        return await _orderRepository.FindByIdAsync(orderId);
    }

    public async Task<OrderResponse> UpdateAsync(int id, Order order)
    {
        var existingOrder = await _orderRepository.FindByIdAsync(id);

        if (existingOrder == null)
        {
            return new OrderResponse("Order not found.");
        }

        //Validate OrderStatusId
        var existingOrderStatus = await _orderStatusRepository.FindByIdAsync(order.OrderStatusId);
        
        if (existingOrderStatus == null)
        {
            return new OrderResponse("Invalid OrderStatusId.");
        }
        //IF ANY ERROR, MAYBE UPDATING STATUSID
        existingOrder.OrderCode = order.OrderCode;
        existingOrder.OrderStatusId= order.OrderStatusId;
        existingOrder.OriginCountry = order.OriginCountry;
        existingOrder.OrderDestination = order.OrderDestination;
        existingOrder.RequestDate = order.RequestDate;
        existingOrder.DeadlineDate = order.DeadlineDate;
        existingOrder.CurrentProcess = order.CurrentProcess;

        try
        {
            _orderRepository.Update(existingOrder);
            await _unitOfWork.CompleteAsync();

            return new OrderResponse(existingOrder);
        }
        catch (Exception e)
        {
            return new OrderResponse($"An error occurred while updating the order: {e.Message}");
        }
    }

    public async Task<OrderResponse> DeleteAsync(int id)
    {
        var existingOrder = await _orderRepository.FindByIdAsync(id);
        
        if(existingOrder == null)
            return new OrderResponse("Order not found.");
        try
        {
            _orderRepository.Remove(existingOrder);
            await _unitOfWork.CompleteAsync();
            
            return new OrderResponse(existingOrder);
        }
        catch (Exception e)
        {
            return new OrderResponse($"An error occurred while deleting the order: {e.Message}");
        }
    }
}