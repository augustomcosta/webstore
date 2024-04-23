using System.ComponentModel.DataAnnotations;
using WebStore.Domain.Entities.Base;

namespace WebStore.Domain.Entities.OrderAggregate;

public sealed class DeliveryMethod : BaseEntity
{
    public DeliveryMethod()
    {
    }

    public DeliveryMethod(Guid id) : base(id)
    {
    }

    [Required] public string Name { get; private set; } = "";

    [Required] public string DeliveryTime { get; private set; } = "";

    [Required] public string Description { get; private set; } = "";

    [Required] public decimal Price { get; set; }
}