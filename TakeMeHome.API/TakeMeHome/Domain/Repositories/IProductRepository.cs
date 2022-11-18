using TakeMeHome.API.TakeMeHome.Domain.Models;

namespace TakeMeHome.API.TakeMeHome.Domain.Repositories;

public interface IProductRepository
{
    Task<IEnumerable<Product>> ListAsync();
    Task AddAsync(Product product);
    Task<Product> FindByIdAsync(int id);
    Task<IEnumerable<Product>> FindByUserIdAndStatusIdAsync(int userId, int statusId);
    void Update(Product product);
    void Remove(Product product);

}