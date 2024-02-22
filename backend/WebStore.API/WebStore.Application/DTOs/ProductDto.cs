using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;


namespace WebStore.API.DTOs;

[DataContract]
public record ProductDto(
    [Required]
    [MinLength(5)]
    [StringLength(30)]
    string Name,
    
    [Required]
    [MinLength(5)]
    [StringLength(50)]
    string Description, 
    
    [Required]
    decimal Price, 
    
    [Required]
    [MinLength(5)]
    [StringLength(300)]
    string ImageUrl,
    
    [property: JsonIgnore]
    string? BrandName,
    
    [Required]
    Guid BrandId,
    
    [Required]
    Guid CategoryId
    
    ) { }