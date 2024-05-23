using System.ComponentModel.DataAnnotations;
using WebStore.Domain.Entities.Base;

namespace WebStore.Domain.Entities.OrderAggregate;

public sealed class DeliveryMethod : BaseEntity
{
    [Required] public string Id { get; private set; } = new Guid().ToString();
    [Required] public string Name { get; private set; } = "";
    [Required] public string DeliveryTime { get; private set; } = "";
    [Required] public string Description { get; private set; } = "";
    [Required] public double Price { get; set; }
}