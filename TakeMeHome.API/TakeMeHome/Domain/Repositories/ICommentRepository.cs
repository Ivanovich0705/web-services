using TakeMeHome.API.TakeMeHome.Domain.Models;

namespace TakeMeHome.API.TakeMeHome.Domain.Repositories;

public interface ICommentRepository
{
    Task<IEnumerable<Comment>> ListAsync();
    Task AddAsync(Comment comment);
    Task<Comment> FindByIdAsync(int id);
    Task<IEnumerable<Comment>> FindByUserId(int userId);
    void Update(Comment comment);
    void Remove(Comment comment);
}