using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Microsoft.EntityFrameworkCore;

namespace WebStore.API.DTOs;

[DataContract]
[Keyless]
[ComplexType]
public record AddressVoDto(
    [Required]
    [MinLength(3)]
    [StringLength(100)]
    string Street,
    
    [Required]
    [MinLength(3)]
    [StringLength(100)]
    string Neighborhood,
    
    [Required]
    [MinLength(3)]
    [StringLength(100)]
    string City,
    
    [Required]
    [MinLength(3)]
    [StringLength(100)]
    string State,
    
    [Required]
    string ZipCode,
    
    [Required]
    [MinLength(1)]
    [StringLength(10)]
    string Number
    );