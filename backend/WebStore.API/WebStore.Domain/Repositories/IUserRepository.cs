using WebStore.Domain.Entities;
using WebStore.Domain.Repositories.Base;

namespace WebStore.Domain.Repositories;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAll();
    Task<User> GetById(string? id);
    Task<User> Create(User user);
    Task<User> Update(string? id, User user);
    Task<User> Delete(string? id);
}