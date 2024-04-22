using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using WebStore.Domain.Entities;


namespace WebStore.API.DTOs;

[DataContract]
public record WishlistDto(
    [Required] string Id,
    [Required] string UserId,
    List<WishlistItemDto>? WishlistItems
    );