using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace WebStore.API.DTOs;

[DataContract]
public record UserDto(
    [Required]
    [MinLength(3)]
    [StringLength(50)]
    string FirstName,
    [Required]
    [MinLength(3)]
    [StringLength(50)]
    string LastName,
    
    [Required]
    [MinLength(5)]
    [StringLength(20)]
    string Username,
    
    [Required]
    [MinLength(8)]
    [StringLength(200)]
    string Password,
    
    [Required]
    [MinLength(5)]
    [StringLength(100)]
    string Email,
    
    [Required]
    [MinLength(11)]
    [StringLength(11)]
    string Cpf
) {}
