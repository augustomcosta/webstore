using System.Runtime.Serialization;

namespace WebStore.API.DTOs.OrderDtoAggregate;

[DataContract]
public record OrderItemVoDto(
 int Quantity,
 decimal Price,
 string ProductName,
 string ProductImgUrl,
 string Brand,
 string Category 
);