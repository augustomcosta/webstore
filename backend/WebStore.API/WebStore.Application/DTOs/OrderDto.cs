using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using WebStore.Domain.Entities;
using WebStore.Domain.Entities.OrderAggregate;
using WebStore.Domain.Entities.OrderAggregate.ValueObjects;
using WebStore.Domain.ValueObjects;

namespace WebStore.API.DTOs;

[DataContract]
public record OrderDto(
    [Required] decimal SubTotal,

    [Required] string BuyerEmail,

    [Required] IReadOnlyList<OrderItemVO> OrderItems,

    [Required] DateTime OrderDate,

    [Required] DeliveryMethod DeliveryMethod,

    [Required] AddressVO ShippingAddress,

    [Required] Guid DeliveryMethodId,

    [Required] decimal Total,

    [Required] Guid UserId,

    [Required] User User
);
