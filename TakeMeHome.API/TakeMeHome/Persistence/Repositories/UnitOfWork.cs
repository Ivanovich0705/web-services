using TakeMeHome.API.TakeMeHome.Domain.Repositories;
using TakeMeHome.API.TakeMeHome.Persistence.Contexts;

namespace TakeMeHome.API.TakeMeHome.Persistence.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    
    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task CompleteAsync()
    {
        await _context.SaveChangesAsync();
    }

}