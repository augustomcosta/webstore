using System.Runtime.Serialization;

namespace WebStore.API.DTOs.OrderDtoAggregate;


[DataContract]
public record OrderItemVoDto(
 string Id,
 int Quantity,
 double Price,
 string ProductName,
 string ProductImgUrl,
 string Brand,
 string Category
);

