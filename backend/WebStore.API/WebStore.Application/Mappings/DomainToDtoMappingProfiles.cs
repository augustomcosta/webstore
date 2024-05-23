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
        CreateMap<Order, OrderDto>().ForMember(dest => dest.orderItems, opt => opt.MapFrom(src => src.OrderItems))
            .ConstructUsing(src => new OrderDto(
                src.Id,
                src.SubTotal,
                src.BuyerEmail,
                null!,
                src.TotalItemQuantity,
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
        CreateMap<OrderItemVO, OrderItemVoDto>().ConstructUsing(src => new OrderItemVoDto(
            src.Id,
            src.Quantity,
            src.Price,
            src.ProductName,
            src.ProductImgUrl,
            src.Brand,
            src.Category
            )).ReverseMap();
    }
}