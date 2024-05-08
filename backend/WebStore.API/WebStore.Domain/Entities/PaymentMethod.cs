using WebStore.Domain.Entities.Base;

namespace WebStore.Domain.Entities;

public class PaymentMethod : BaseEntity
{
    public string? Name { get; set; }

    public PaymentMethod(Guid id) : base(id) { }
}