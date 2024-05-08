using System.ComponentModel.DataAnnotations;

namespace WebStore.API.DTOs;

public record PaymentMethodDto(
    [Required]
    string Name
    );
