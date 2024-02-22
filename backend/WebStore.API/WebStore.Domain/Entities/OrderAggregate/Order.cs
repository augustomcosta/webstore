using System.ComponentModel.DataAnnotations;
using WebStore.Domain.Entities.Base;
using WebStore.Domain.Entities.OrderAggregate.ValueObjects;
using WebStore.Domain.Validation;
using WebStore.Domain.ValueObjects;

namespace WebStore.Domain.Entities.OrderAggregate;

public sealed class Order : BaseEntity
{
    [Required]
    public decimal SubTotal { get; private set; }
    
    [Required]
    public string BuyerEmail { get; private set; }
    
    [Required]
    public IReadOnlyList<OrderItemVO> OrderItems { get; private set; }

    [Required] 
    public DateTime OrderDate { get; private set; } = DateTime.Now;
    
    [Required]
    public DeliveryMethod DeliveryMethod { get; private set; }
    
    [Required]
    public AddressVO ShippingAddress { get; private set; }
    
    [Required]
    public Guid DeliveryMethodId { get; private set; }
    
    [Required]
    public decimal Total { get; private set; }
    
    [Required]
    public Guid UserId { get; private set; } 
    
    [Required]
    public User User { get; private set; }
    
    public Order(Guid id, decimal subTotal, IReadOnlyList<OrderItemVO> orderItems, DeliveryMethod deliveryMethod, AddressVO shippingAddress) : base(id)
    {
        DeliveryMethod = deliveryMethod;
        OrderItems = orderItems;
        ShippingAddress = shippingAddress;
        Total = GetTotal(subTotal);
    }

    private void ValidateSubTotal(decimal subTotal)
    {
        DomainValidationException.When(decimal.IsNegative(subTotal),"Total order price should be positive.");
        SubTotal = subTotal;
    }

    private decimal GetTotal(decimal subTotal)
    {
        var total = subTotal + DeliveryMethod.Price;
        if (total < 0)
        {
            throw new Exception("Order total should be positive.");
        }
        return total;
    }
}