using System.Runtime.Serialization;

namespace WebStore.API.DTOs.BasketDtoAggregate;

[DataContract]
public record BasketItemDto(
    string Id,
    int Quantity,
    string ProductName,
    string ProductImgUrl,
    double Price,
    string Brand,
    string Category
);