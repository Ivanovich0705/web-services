using TakeMeHome.API.TakeMeHome.Domain.Models;
using TakeMeHome.API.TakeMeHome.Domain.Repositories;
using TakeMeHome.API.TakeMeHome.Domain.Services;
using TakeMeHome.API.TakeMeHome.Domain.Services.Communication;

namespace TakeMeHome.API.TakeMeHome.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    
    private readonly IUnitOfWork _unitOfWork;

    public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<User>> ListAsync()
    {
        return await _userRepository.ListAsync();
    }

    public async Task<User> FindByIdAsync(int id)
    {
        return await _userRepository.FindByIdAsync(id);
    }

    public async Task<User> FindByUserNameAsync(string userName)
    {
        return await _userRepository.FindByUserNameAsync(userName);
    }
    
    public async Task<User> FindByEmailAndPasswordAsync(string email, string password)
    {
        return await _userRepository.FindByEmailAndPasswordAsync(email, password);
    }
    public async Task<UserResponse> SaveAsync(User user)
    {
        try
        {
            await _userRepository.AddAsync(user);
            await _unitOfWork.CompleteAsync();
            
            return new UserResponse(user);
        }
        catch (Exception e)
        {
            return new UserResponse($"An error occurred when saving the user: {e.Message}");
        }
    }

    public async Task<UserResponse> UpdateAsync(int id, User user)
    {
        var existingUser = await _userRepository.FindByIdAsync(id);
        
        if(existingUser == null)
            return new UserResponse("User not found.");

        existingUser.FullName = user.FullName;
        existingUser.Username = user.Username;
        existingUser.Password = user.Password;
        existingUser.Email = user.Email;
        existingUser.Country = user.Country;
        existingUser.DateOfBirth = user.DateOfBirth;
        existingUser.Phone = user.Phone;
        existingUser.Description = user.Description;
        existingUser.PhotoUrl = user.PhotoUrl;
        existingUser.Points = user.Points;
        existingUser.Rating = user.Rating;
        existingUser.IdNumber = user.IdNumber;
        
        try
        {
            _userRepository.Update(existingUser);
            await _unitOfWork.CompleteAsync();
            
            return new UserResponse(existingUser);
        }
        catch (Exception e)
        {
            return new UserResponse($"An error occurred while updating the user: {e.Message}");
        }
        
    }

    public async Task<UserResponse> DeleteAsync(int id)
    {
        var existingUser = await _userRepository.FindByIdAsync(id);
        
        if(existingUser == null)
            return new UserResponse("User not found.");
        try
        {
            _userRepository.Remove(existingUser);
            await _unitOfWork.CompleteAsync();
            
            return new UserResponse(existingUser);
        }
        catch (Exception e)
        {
            return new UserResponse($"An error occurred while deleting the user: {e.Message}");
        }
        
    }
}