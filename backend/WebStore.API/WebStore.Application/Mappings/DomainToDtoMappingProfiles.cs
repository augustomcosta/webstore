using AutoMapper;
using WebStore.API.DTOs;
using WebStore.API.DTOs.BasketDtoAggregate;
using WebStore.API.DTOs.UserDto;
using WebStore.Domain.Entities;
using WebStore.Domain.Entities.OrderAggregate;
using WebStore.Domain.ValueObjects;

namespace WebStore.API.Mappings;

public class DomainToDtoMappingProfiles : Profile
{
    public DomainToDtoMappingProfiles()
    {
        CreateMap<Product, ProductDto>()
            .ConstructUsing(src => new ProductDto(
                src.Id,
                src.Name,
                src.Description,
                src.Price,
                src.ImageUrl,
                src.BrandName,
                src.CategoryName,
                src.BrandId,
                src.CategoryId
            )).ReverseMap();
        CreateMap<ProductBrand, BrandDto>().ReverseMap();
        CreateMap<ProductCategory, CategoryDto>().ReverseMap();
        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<Order, UserDto>().ReverseMap();
        CreateMap<Basket, BasketDto>().ReverseMap();
        CreateMap<Basket, BasketUpdateDto>()
            .ForMember(dest => dest.BasketItems, opt => opt.MapFrom(src => src.BasketItems))
            .ConstructUsing(src => new BasketUpdateDto(
                src.Id,
                null,
                src.DeliveryMethodId.ToString(),
                src.PaymentIntentId,
                src.ShippingPrice,
                src.TotalPrice)
            )
            .ReverseMap();
        CreateMap<BasketItem, BasketItemDto>().ConstructUsing(src => new BasketItemDto(
            src.Id,
            src.Quantity,
            src.ProductName,
            src.ProductImgUrl,
            src.Price,
            src.Brand,
            src.Category
        )).ReverseMap();
        CreateMap<Wishlist, WishlistDto>().ReverseMap();
        CreateMap<WishlistItem, WishlistItemDto>().ConstructUsing(src => new WishlistItemDto(
            src.Id,
            src.ProductName,
            src.ProductImgUrl,
            src.Price,
            src.Brand,
            src.Category
        )).ReverseMap();
        CreateMap<DeliveryMethod, DeliveryMethodDto>().ReverseMap();
        CreateMap<AddressVO, AddressVoDto>().ReverseMap();
    }
}