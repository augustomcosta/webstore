using WebStore.API.DTOs;
using WebStore.Domain.Entities;

namespace WebStore.API.Interfaces;

public interface IWishlistService
{
    Task<WishlistDto> GetWishlistAsync(string? id);
    Task<WishlistDto> UpdateWishlistAsync(Wishlist wishlist, string userId);
    Task DeleteWishlistAsync(string? id);
    Task<WishlistDto> GetWishlistByUserIdAsync(string? userId);
}