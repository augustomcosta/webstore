using Microsoft.EntityFrameworkCore;
using WebStore.Domain.Entities;
using WebStore.Domain.Repositories;
using WebStore.Infra.Context;

namespace WebStore.Data.RepositoriesImpl;

public class WishlistRepository : IWishlistRepository
{
    private readonly AppDbContext _context;
    
    public WishlistRepository(AppDbContext context)
    {
        _context = context;
    }
    

    public async Task<Wishlist> GetWishlistAsync(string? id)
    {
        var wishlist = await _context.Wishlists!.FirstOrDefaultAsync(w => w.Id == id);
        if (wishlist is null)
        {
            throw new Exception($"Wishlist with id {id} was not found.");
        }

        return wishlist;
    }
    

    public async Task<Wishlist> UpdateWishlistAsync(Wishlist wishlist, string userId)
    {
        if (wishlist.Id is null)
        {
            throw new ArgumentException("Wishlist Id cannot be null", nameof(wishlist.Id));
        }

        var wishlistToUpdate = await _context.Wishlists!.FirstOrDefaultAsync(w => w.Id == wishlist.Id);
        if (wishlistToUpdate is null)
        {
            wishlistToUpdate = new Wishlist();
            wishlist.UpdateWishlist(wishlistToUpdate);
            
            var user = await _context.Users!.FirstOrDefaultAsync(u => u.Id == userId);
            if (user != null)
            {
                user.WishlistId = wishlistToUpdate.Id; 
            }

            _context.Add(wishlistToUpdate);
            
            await _context.SaveChangesAsync();
            
            return wishlistToUpdate;
        }
        
        wishlist.UpdateWishlist(wishlistToUpdate);

        await _context.SaveChangesAsync();

        return wishlistToUpdate;
    }

    public async Task<Wishlist> Delete(string? wishlistId)
    {
        var wishlistToDelete = await GetWishlistAsync(wishlistId);

        _context.Wishlists!.Remove(wishlistToDelete);

        await _context.SaveChangesAsync();
        return wishlistToDelete;
    }

    public async Task<Wishlist> GetWishlistByUserId(string? userId)
    {
        var wishlist = await _context.Wishlists!.FirstOrDefaultAsync(w => w.UserId == userId);
        if (wishlist is null)
        {
            throw new Exception($"No wishlist for User Id {userId} was found.");
        }
        
        return wishlist;
    }
}