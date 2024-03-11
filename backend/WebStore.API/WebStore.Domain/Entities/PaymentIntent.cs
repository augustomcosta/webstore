using WebStore.Domain.Entities.Base;

namespace WebStore.Domain.Entities;

public class PaymentIntent : BaseEntity
{
    public string? Name { get; set; }

    public PaymentIntent(Guid id) : base(id) { }
}