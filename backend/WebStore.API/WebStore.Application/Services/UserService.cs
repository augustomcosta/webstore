using AutoMapper;
using WebStore.API.DTOs;
using WebStore.API.DTOs.UserDto;
using WebStore.API.Interfaces;
using WebStore.Domain.Entities;
using WebStore.Domain.Repositories;
using WebStore.Domain.ValueObjects;

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

    public async Task<UserDto> GetById(string? id)
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

    public async Task Update(string? id, UserDto userDto)
    {
        var userEntity = _mapper.Map<User>(userDto);
        await _repository.Update(id,userEntity);
    }

    public async Task Delete(string? id)
    {
        await _repository.Delete(id);
    }

    public async Task<AddressVoDto> UpdateUserAddress(string id, AddressVoDto addressVoDto)
    {
        var userAddress = _mapper.Map<AddressVO>(addressVoDto);
        
        var address = await _repository.UpdateUserAddress(id, userAddress);
        
        return _mapper.Map<AddressVoDto>(address);
    }
    
    public async Task<AddressVoDto> GetUserAddress(string? id)
    {
        var userAddress = await _repository.GetUserAddress(id);
        
        return _mapper.Map<AddressVoDto>(userAddress);
    }
}