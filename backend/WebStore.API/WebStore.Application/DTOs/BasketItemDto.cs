using System.Runtime.Serialization;

namespace WebStore.Domain.Entities;

[DataContract]
public record BasketItemDto(
    string Id,
    int Quantity,
    string ProductName,
    string ProductId,
    string ProductImgUrl,
    decimal Price,
    string Brand,
    string Category
);