using AutoMapper;
using WebStore.API.DTOs;
using WebStore.API.Interfaces;
using WebStore.Domain.Entities;
using WebStore.Domain.Repositories;

namespace WebStore.API.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;
    
    public UserService(IUserRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<UserDto>> GetAll()
    {
        var userEntites = await _repository.GetAll();
        return _mapper.Map<IEnumerable<UserDto>>(userEntites);
    }

    public async Task<UserDto> GetById(Guid? id)
    {
        var userEntity = await _repository.GetById(id);
        return _mapper.Map<UserDto>(userEntity);
    }

    public async Task<UserDto> Create(UserDto userDto)
    {
        var userEntity = _mapper.Map<User>(userDto);
        await _repository.Create(userEntity);
        return userDto;
    }

    public async Task Update(Guid? id, UserDto userDto)
    {
        var userEntity = _mapper.Map<User>(userDto);
        await _repository.Update(id,userEntity);
    }

    public async Task Delete(Guid? id)
    {
        await _repository.Delete(id);
    }
}