using WebStore.Domain.Entities;
using WebStore.Domain.Repositories.Base;
using WebStore.Domain.ValueObjects;

namespace WebStore.Domain.Repositories;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAll();
    Task<User> GetById(string? id);
    Task<User> Create(User user);
    Task<User> Update(string? id, User user);
    Task<User> Delete(string? id);
    Task<User> UpdateUserAddress(string? id,AddressVO address);
}