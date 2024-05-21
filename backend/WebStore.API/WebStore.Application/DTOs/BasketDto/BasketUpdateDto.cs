using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using WebStore.API.DTOs.BasketDtoAggregate;

namespace WebStore.API.DTOs.BasketDto;

[DataContract]
public record BasketUpdateDto(
    string Id,
    [Required] List<BasketItemDto> BasketItems,
    string DeliveryMethodId,
    decimal ShippingPrice,
    decimal TotalPrice
    );