using Microsoft.EntityFrameworkCore;
using WebStore.Domain.Entities;
using WebStore.Domain.Repositories;
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
        var users = await _context.Users.ToListAsync();
        return users;
    }

    public async Task<User> GetById(Guid? id)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        return user;
    }

    public async Task<User> Create(User user)
    {
        var userToCreate = await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<User> Update(Guid? id, User user)
    {
        var userToUpdate = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        if (userToUpdate == null)
        {
            throw new Exception($"User with Id {id} not found.");
        }
        
        user.UpdateUser(userToUpdate);
        return user;
    }

    public async Task<User> Delete(Guid? id)
    {
        var userToDelete = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        _context.Users.Remove(userToDelete);
        await _context.SaveChangesAsync();
        return userToDelete;
    }
}