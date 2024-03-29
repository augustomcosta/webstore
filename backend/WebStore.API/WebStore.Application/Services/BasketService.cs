﻿using AutoMapper;
using WebStore.API.DTOs;
using WebStore.API.Interfaces;
using WebStore.Domain.Entities;
using WebStore.Domain.Repositories;

namespace WebStore.API.Services;

public class BasketService : IBasketService
{
    private readonly IBasketRepository _basketRepo;
    private readonly IMapper _mapper;

    public BasketService(IBasketRepository basketRepo, IMapper mapper)
    {
        _basketRepo = basketRepo;
        _mapper = mapper;
    }

    public async Task<BasketDto> CreateBasketAsync(Guid userId)
    {
        var basket = await _basketRepo.CreateBasketAsync(userId);

        return _mapper.Map<BasketDto>(basket);
    }

    public async Task<BasketDto> GetBasketAsync(Guid id)
    {
        var basket = await _basketRepo.GetBasketAsync(id);
        if (basket is null) throw new Exception("Basket not found");
        return _mapper.Map<BasketDto>(basket);
    }

    public async Task<BasketDto> UpdateBasketAsync(Guid basketId, Basket basket)
    {
        var updatedBasket = await _basketRepo.UpdateBasketAsync(basketId, basket);
        if (updatedBasket is null) throw new Exception("Basket not found");
        return _mapper.Map<BasketDto>(updatedBasket);
    }

    public async Task DeleteBasketAsync(Guid id)
    {
        await _basketRepo.Delete(id);
    }
}