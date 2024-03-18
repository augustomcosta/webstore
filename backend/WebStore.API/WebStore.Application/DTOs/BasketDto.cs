using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using WebStore.Domain.Entities;

namespace WebStore.API.DTOs;

[DataContract]
public record BasketDto(
    [Required] List<BasketItem> BasketItems,
    [Required] string DeliveryMethodId,
    [Required] string PaymentIntentId,
    decimal ShippingPrice,
    decimal TotalPrice
);