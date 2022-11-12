using TakeMeHome.API.TakeMeHome.Domain.Models;
using TakeMeHome.API.TakeMeHome.Domain.Services.Communication;

namespace TakeMeHome.API.TakeMeHome.Domain.Services;

public interface IProductService
{
    Task<IEnumerable<Product>> ListAsync();
    Task<ProductResponse> SaveAsync(Product product);
    Task<ProductResponse> UpdateAsync(int id, Product product);
    Task<ProductResponse> DeleteAsync(int id);
}