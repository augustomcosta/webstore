using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace WebStore.API.DTOs.BasketDtoAggregate;

[DataContract]
public record BasketUpdateDto(
    string Id,
    [Required] List<BasketItemDto> BasketItems,
    string DeliveryMethodId,
    string PaymentIntentId,
    decimal ShippingPrice,
    decimal TotalPrice
    );