using WebStore.API.DTOs;
using WebStore.API.Interfaces.Base;

namespace WebStore.API.Interfaces;

public interface IUserService 
{
    Task<IEnumerable<UserDto>> GetAll();
    Task<UserDto> GetById(string? id);
    Task<UserDto> Create(UserDto userDto);
    Task Update(string? id, UserDto userDto);
    Task Delete(string? id);
}