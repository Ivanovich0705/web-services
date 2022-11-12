namespace TakeMeHome.API.TakeMeHome.Domain.Repositories;

public interface IUnitOfWork
{
    Task CompleteAsync();
}