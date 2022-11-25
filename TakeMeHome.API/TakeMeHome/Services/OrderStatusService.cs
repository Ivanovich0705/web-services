using TakeMeHome.API.TakeMeHome.Domain.Models;
using TakeMeHome.API.TakeMeHome.Domain.Repositories;
using TakeMeHome.API.TakeMeHome.Domain.Services;
using TakeMeHome.API.TakeMeHome.Domain.Services.Communication;

namespace TakeMeHome.API.TakeMeHome.Services;

public class OrderStatusService : IOrderStatusService
{
    private readonly IOrderStatusRepository _orderStatusRepository;
    
    private readonly IUnitOfWork _unitOfWork;

    public OrderStatusService(IOrderStatusRepository orderStatusRepository, IUnitOfWork unitOfWork)
    {
        _orderStatusRepository = orderStatusRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<OrderStatus>> ListAsync()
    {
        return await _orderStatusRepository.ListAsync();
    }
    
    public async Task<OrderStatusResponse> FindByIdAsync(int id)
    {
        var existingOrderStatus = await _orderStatusRepository.FindByIdAsync(id);
        if (existingOrderStatus == null)
            return new OrderStatusResponse("OrderStatus not found.");

        return new OrderStatusResponse(existingOrderStatus);
    }

    public async Task<OrderStatusResponse> SaveAsync(OrderStatus orderStatus)
    {
        try
        {
            await _orderStatusRepository.AddAsync(orderStatus);
            await _unitOfWork.CompleteAsync();
            
            return new OrderStatusResponse(orderStatus);
        }
        catch (Exception e)
        {
            return new OrderStatusResponse($"An error occurred while saving the order status: {e.Message}");
        }
    }

    public async Task<OrderStatusResponse> UpdateAsync(int id, OrderStatus orderStatus)
    {
        var existingOrderStatus = await _orderStatusRepository.FindByIdAsync(id);
        
        if(existingOrderStatus == null)
            return new OrderStatusResponse("Order Status not found.");

        existingOrderStatus.Status = orderStatus.Status;

        try
        {
            _orderStatusRepository.Update(existingOrderStatus);
            await _unitOfWork.CompleteAsync();
            
            return new OrderStatusResponse(existingOrderStatus);
        }
        catch (Exception e)
        {
            return new OrderStatusResponse($"An error occurred while updating the order status: {e.Message}");
        }
    }

    public async Task<OrderStatusResponse> DeleteAsync(int id)
    {
        var existingOrderStatus = await _orderStatusRepository.FindByIdAsync(id);
        
        if(existingOrderStatus == null)
            return new OrderStatusResponse("Order Status not found.");
        try
        {
            _orderStatusRepository.Remove(existingOrderStatus);
            await _unitOfWork.CompleteAsync();
            
            return new OrderStatusResponse(existingOrderStatus);
        }
        catch (Exception e)
        {
            return new OrderStatusResponse($"An error occurred while deleting the order status: {e.Message}");
        }
    }
}