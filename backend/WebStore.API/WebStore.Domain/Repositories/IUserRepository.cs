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
    Task<AddressVO> UpdateUserAddress(string? id,AddressVO address);
    Task<AddressVO> GetUserAddress(string? id);
}