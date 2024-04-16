using WebStore.Domain.Entities;
using WebStore.Domain.Repositories.Base;

namespace WebStore.Domain.Repositories;

public interface IWishlistRepository
{
    Task<Wishlist> GetWishlistAsync(string? id);
    Task<Wishlist> UpdateWishlistAsync(Wishlist wishlist);
    Task<Wishlist> Delete(string? id);
}