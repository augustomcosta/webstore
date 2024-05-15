using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace WebStore.API.DTOs;

[DataContract]
public record DeliveryMethodDto(
    Guid Id,
    [Required]string Name,
    
    [Required] string DeliveryTime,

    [Required]  string Description,

    [Required]  decimal Price
    );