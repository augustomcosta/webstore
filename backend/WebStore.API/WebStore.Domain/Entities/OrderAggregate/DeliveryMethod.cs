using System.ComponentModel.DataAnnotations;
using WebStore.Domain.Entities.Base;

namespace WebStore.Domain.Entities.OrderAggregate;

public class DeliveryMethod : BaseEntity
{
    [Required]
    public string Name { get; private set; } = "";
    
    [Required]
    public string DeliveryTime { get; private set; } = "";
    
    [Required]
    public string Description { get; private set; } = "";
    
    [Required]
    public decimal Price { get; private set; }
}