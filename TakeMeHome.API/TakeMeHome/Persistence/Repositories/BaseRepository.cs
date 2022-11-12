using TakeMeHome.API.TakeMeHome.Persistence.Contexts;

namespace TakeMeHome.API.TakeMeHome.Persistence.Repositories;

public class BaseRepository
{
    protected readonly AppDbContext _context;

    public BaseRepository(AppDbContext context)
    {
        _context = context;
    }
}