using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using WebStore.Domain.Entities;

namespace WebStore.API.DTOs;

[DataContract]
public record BasketDto (
    [Required]
    Guid Id,
    
    [Required]
    List<BasketItem> BasketItems,
    
    [Required]
    int DeliveryMethodId,
    
    [Required]
    string PaymentIntentId
    );