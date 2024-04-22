using System.Runtime.Serialization;

namespace WebStore.API.DTOs;

[DataContract]
public record WishlistItemDto(
    string Id,
    string ProductName,
    string ProductImgUrl,
    decimal Price,
    string Brand,
    string Category
);