﻿using AutoMapper;
using WebStore.API.DTOs;
using WebStore.Domain.Entities;
using WebStore.Domain.Entities.OrderAggregate;

namespace WebStore.API.Mappings;

public class DomainToDtoMappingProfiles : Profile
{
    public DomainToDtoMappingProfiles()
    {
        CreateMap<Product, ProductDto>()
            .ConstructUsing(src => new ProductDto(
                src.Name,
                src.Description,
                src.Price,
                src.ImageUrl,
                src.BrandName,
                src.BrandId,
                src.CategoryId
            )).ReverseMap();
        CreateMap<ProductBrand, BrandDto>().ReverseMap();
        CreateMap<ProductCategory, CategoryDto>().ReverseMap();
        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<Order, UserDto>().ReverseMap();
        CreateMap<Basket, BasketDto>().ReverseMap();
    }
}