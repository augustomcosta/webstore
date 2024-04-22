using AutoMapper;
using WebStore.API.DTOs;
using WebStore.API.Interfaces;
using WebStore.Domain.Entities;
using WebStore.Domain.Repositories;

namespace WebStore.API.Services;

public class WishlistService : IWishlistService
{
    private readonly IWishlistRepository _repository;
    private readonly IMapper _mapper;

    public WishlistService(IWishlistRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;

    }

    public async Task<WishlistDto> GetWishlistAsync(string? id)
    {
        var wishlist = await _repository.GetWishlistAsync(id);
        
        if (wishlist is null) throw new Exception($"Wishlist with Id {id} was not found.");
        
        return _mapper.Map<WishlistDto>(wishlist);
    }

    public async Task<WishlistDto> UpdateWishlistAsync(Wishlist wishlist)
    {
        var updatedWishlist = await _repository.UpdateWishlistAsync(wishlist);
        
        if (updatedWishlist is null) throw new Exception("Wishlist not found");

        return _mapper.Map<WishlistDto>(updatedWishlist);

    }

    public async Task DeleteWishlistAsync(string? id)
    {
        await _repository.Delete(id);
    }

    public async Task<WishlistDto> GetByUserId(string userId)
    {
        var wishlist = await _repository.GetByUserId(userId);
        return _mapper.Map<WishlistDto>(wishlist);
    }
}