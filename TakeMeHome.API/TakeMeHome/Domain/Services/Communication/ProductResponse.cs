using TakeMeHome.API.Shared.Domain.Services.Communication;
using TakeMeHome.API.TakeMeHome.Domain.Models;

namespace TakeMeHome.API.TakeMeHome.Domain.Services.Communication;

public class ProductResponse : BaseResponse<Product>
{
    public ProductResponse(Product resource) : base(resource)
    {
    }

    public ProductResponse(string message) : base(message)
    {
    }
}