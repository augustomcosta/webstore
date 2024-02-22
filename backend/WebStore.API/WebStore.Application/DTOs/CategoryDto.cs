using System.ComponentModel.DataAnnotations;
using WebStore.Domain.Entities;

namespace WebStore.API.DTOs;

public record CategoryDto
{
    [Required]
    [MinLength(5)]
    [StringLength(30)]
    public string? Name { get; set; }
    
    public ICollection<Product>? Products { get; set; }
}