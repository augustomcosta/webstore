﻿using System.Collections;
using System.ComponentModel.DataAnnotations;
using WebStore.Domain.Entities;

namespace WebStore.API.DTOs;

public record BrandDto
{
    [Required]
    [MinLength(5)]
    [StringLength(30)]
    public string? Name { get; set; }
    
    [MinLength(5)]
    [StringLength(300)]
    public string? ImageUrl { get; set; }
    
    public ICollection<Product>? Products { get; set; }
}