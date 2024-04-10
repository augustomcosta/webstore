using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using WebStore.Domain.Entities;

namespace WebStore.API.DTOs;

[DataContract]
public record BasketDto(
    string Id,
    [Required] List<BasketItemDto> BasketItems,
    string DeliveryMethodId,
    string PaymentIntentId,
    string UserId,
    decimal ShippingPrice,
    decimal TotalPrice,
    DateTime CreatedAt
);