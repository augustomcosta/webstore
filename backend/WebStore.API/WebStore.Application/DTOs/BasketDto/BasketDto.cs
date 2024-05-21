using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace WebStore.API.DTOs.BasketDtoAggregate;

[DataContract]
public record BasketDto(
    string Id,
    [Required] List<BasketItemDto> BasketItems,
    string DeliveryMethodId,
    string UserId,
    decimal ShippingPrice,
    decimal TotalPrice,
    DateTime CreatedAt
);