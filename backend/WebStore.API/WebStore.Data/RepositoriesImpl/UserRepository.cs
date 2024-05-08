using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebStore.Domain.Entities;
using WebStore.Domain.Repositories;
using WebStore.Domain.ValueObjects;
using WebStore.Infra.Context;

namespace WebStore.Data.RepositoriesImpl;

public class UserRepository : IUserRepository
{
    
    private readonly AppDbContext _context;
    
    public UserRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<User>> GetAll()
    {
        var users = await _context.Users!.ToListAsync();
        return users;
    }

    public async Task<User> GetById(string? id)
    {
        var user = await _context.Users!.FirstOrDefaultAsync(u => u.Id == id);
        if (user is null)
        {
            throw new Exception($"User with Id {id} was not found");
        }
        
        return user;
    }

    public async Task<User> Create(User user)
    { 
        await _context.Users!.AddAsync(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<User> Update([FromQuery]string? id, [FromBody]User user)
    {
        var userToUpdate = await _context.Users!.FirstOrDefaultAsync(u => u.Id == id);
        if (userToUpdate == null)
        {
            throw new Exception($"User with Id {id} not found.");
        }
        
        user.UpdateUser(userToUpdate);
        return user;
    }

    public async Task<User> Delete(string? id)
    {
        var userToDelete = await _context.Users!.FirstOrDefaultAsync(u => u.Id == id);
        if (userToDelete is null)
        {
            throw new Exception($"User with Id {id} was not found");
        }
        
        _context.Users!.Remove(userToDelete);
        await _context.SaveChangesAsync();
        return userToDelete;
    }

    public async Task<AddressVO> UpdateUserAddress([FromQuery]string? id, [FromBody]AddressVO? address)
    {
        var userToUpdate = await _context.Users!.FirstOrDefaultAsync(u => u.Id == id);
        if (userToUpdate is null)
        {
            throw new Exception($"User with Id {id} was not found");
        }

        userToUpdate.Address = address!;

        await _context.SaveChangesAsync();
        
        return userToUpdate.Address;
    }

    public async Task<AddressVO> GetUserAddress([FromQuery] string? id)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        if ( user is null) throw new Exception("User doesn't exist");

        return user.Address;
    }
    
}