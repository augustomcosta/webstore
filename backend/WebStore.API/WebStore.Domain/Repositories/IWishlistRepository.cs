using WebStore.Domain.Entities;
using WebStore.Domain.Repositories.Base;

namespace WebStore.Domain.Repositories;

public interface IWishlistRepository
{
    Task<Wishlist> CreateNewWishlist(string userId);
    Task<Wishlist> GetWishlistAsync(string? id);
    Task<Wishlist> UpdateWishlistAsync(Wishlist wishlist);
    Task<Wishlist> Delete(string? id);
    Task<Wishlist> GetByUserId(string userId);
}