using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;


namespace WebStore.API.DTOs;

[DataContract]
public record WishlistDto(
    [Required]
    string Id,
    [Required]
    string UserId,
    List<ProductDto>? Products
    );