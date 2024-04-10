using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using WebStore.Domain.ValueObjects;

namespace WebStore.API.DTOs;

[DataContract]
public record UserDto 
{
    [Required]
    [MinLength(3)]
    [StringLength(50)]
    public string FirstName { get; set; } = "";

    [Required]
    [MinLength(3)]
    [StringLength(50)]
    public string LastName { get; set; } = "";

    [Required]
    [MinLength(5)]
    [StringLength(20)]
    public string Username { get; set; } = "";

    [Required]
    [MinLength(8)]
    [StringLength(200)]
    public string Password { get; set; } = "";

    [Required]
    [MinLength(5)]
    [StringLength(100)]
    public string Email { get; set; } = "";

    [MinLength(11)]
    [StringLength(11)]
    public string? Cpf { get; set; } = "";
    
    public AddressVO Address { get; set; } = new AddressVO();
}

