using System.ComponentModel.DataAnnotations;

namespace WebStore.Domain.Entities.Authentication;

public class UserRegistrationModel
{
    [Required(ErrorMessage = "First name is required")]
    [MinLength(3, ErrorMessage = "First name must have at least 3 characters")]
    [StringLength(50, ErrorMessage = "First name must have a maximum of 50 characters")]
    public string? FirstName { get; set; }

    [Required(ErrorMessage = "Last name is required")]
    [MinLength(3, ErrorMessage = "Last name must have at least 3 characters")]
    [StringLength(50, ErrorMessage = "Last name must have a maximum of 50 characters")]
    public string? LastName { get; set; }

    [Required(ErrorMessage = "Username is required")]
    [MinLength(5, ErrorMessage = "Username must have at least 5 characters")]
    [StringLength(20, ErrorMessage = "Username must have a maximum of 20 characters")]
    public string? Username { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    [StringLength(100, ErrorMessage = "Email must have a maximum of 100 characters")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [MinLength(8, ErrorMessage = "Password must have at least 8 characters")]
    [StringLength(200, ErrorMessage = "Password must have a maximum of 200 characters")]
    public string Password { get; set; }

    [MinLength(11, ErrorMessage = "CPF must have 11 characters")]
    [StringLength(11, ErrorMessage = "CPF must have 11 characters")]
    public string? Cpf { get; set; }
}