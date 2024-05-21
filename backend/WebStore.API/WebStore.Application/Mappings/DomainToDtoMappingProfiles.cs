using AutoMapper;
using WebStore.API.DTOs;
using WebStore.API.DTOs.BasketDto;
using WebStore.API.DTOs.BasketDtoAggregate;
using WebStore.API.DTOs.OrderDtoAggregate;
using WebStore.API.DTOs.UserDto;
using WebStore.Domain.Entities;
using WebStore.Domain.Entities.OrderAggregate;
using WebStore.Domain.Entities.OrderAggregate.ValueObjects;
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
        CreateMap<Order, OrderDto>().ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems)).
            ConstructUsing(src => new OrderDto(
                src.SubTotal,
                src.BuyerEmail,
                null!,
                src.OrderDate,
                src.DeliveryMethod,
                src.ShippingAddress,
                src.DeliveryMethodId,
                src.Total,
                src.UserId
                )).
        ReverseMap();
        CreateMap<Basket, BasketDto>().ReverseMap();
        CreateMap<Basket, BasketUpdateDto>()
            .ForMember(dest => dest.BasketItems, opt => opt.MapFrom(src => src.BasketItems))
            .ConstructUsing(src => new BasketUpdateDto(
                src.Id,
                null,
                src.DeliveryMethodId.ToString(),
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
        CreateMap<DeliveryMethod, DeliveryMethodDto>().ConstructUsing(src => new DeliveryMethodDto(
            src.Id,
            src.Name,
            src.DeliveryTime,
            src.Description,
            src.Price)).ReverseMap();
        CreateMap<AddressVO, AddressVoDto>().ConstructUsing(src => new AddressVoDto(
            src.Street,
            src.Neighborhood,
            src.City,
            src.State,
            src.ZipCode,
            src.Number
            )).ReverseMap();
        CreateMap<PaymentMethod, PaymentMethodDto>().ReverseMap();
        CreateMap<OrderItemVO, OrderItemVoDto>()
            .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Brand))
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
            .ForMember(dest => dest.ProductImgUrl, opt => opt.MapFrom(src => src.ProductImgUrl))
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductName))
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity));
    }
}