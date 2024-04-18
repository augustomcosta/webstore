using WebStore.Domain.Entities;
using WebStore.Domain.Repositories.Base;

namespace WebStore.Domain.Repositories;

public interface IWishlistRepository
{
    Task<Wishlist> GetWishlistAsync(string? id);
    Task<Wishlist> UpdateWishlistAsync(Wishlist wishlist, string userId);
    Task<Wishlist> Delete(string? id);
    Task<Wishlist> GetWishlistByUserId(string? userId);
}