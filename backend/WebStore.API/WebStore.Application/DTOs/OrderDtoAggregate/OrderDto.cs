using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using WebStore.Domain.Entities.OrderAggregate;
using WebStore.Domain.ValueObjects;

namespace WebStore.API.DTOs.OrderDtoAggregate;

[DataContract]
public record OrderDto(
    [Required] decimal SubTotal,

    [Required] string BuyerEmail,

    [Required] List<OrderItemVoDto> OrderItems,

    [Required] DateTime OrderDate,

    [Required] DeliveryMethod DeliveryMethod,

    [Required] AddressVO ShippingAddress,

    [Required] string DeliveryMethodId,

    [Required] decimal Total,

    [Required] string UserId
);
