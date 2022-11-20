using TakeMeHome.API.TakeMeHome.Domain.Models;

namespace TakeMeHome.API.TakeMeHome.Domain.Repositories;

public interface ICommentRepository
{
    Task<IEnumerable<Comment>> ListAsync();
    Task AddAsync(Comment comment);
    Task<Comment> FindByIdAsync(int id);
    Task<IEnumerable<Comment>> FindByUserId(int userId);
    Task<IEnumerable<Comment>> ListByOrderAndUserId(int id);

    void Update(Comment comment);
    void Remove(Comment comment);
}