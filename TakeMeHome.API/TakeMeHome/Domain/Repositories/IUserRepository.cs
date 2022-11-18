using TakeMeHome.API.TakeMeHome.Domain.Models;

namespace TakeMeHome.API.TakeMeHome.Domain.Repositories;

public interface IUserRepository
{
    Task<IEnumerable<User>> ListAsync();
    Task AddAsync(User user);
    Task<User> FindByIdAsync(int id);
    Task<User> FindByUserNameAsync(string userName);
    Task<User> FindByEmailAndPasswordAsync(string email, string password);
    Task<User> FindByEmailAsync(string email);
    void Update(User user);
    void Remove(User user);
}