using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using WebStore.Domain.Entities.Base;
using WebStore.Domain.Entities.OrderAggregate.ValueObjects;
using WebStore.Domain.Validation;
using WebStore.Domain.ValueObjects;

namespace WebStore.Domain.Entities.OrderAggregate;

public sealed class Order : BaseEntity
{
    public Order(string userId, ICollection<OrderItemVO> orderItems)
    {
        OrderItems = orderItems;
        UserId = userId;
    }

    public Order(Guid id, decimal subTotal, ICollection<OrderItemVO> orderItems, DeliveryMethod deliveryMethod
        ) : base(id)
    {
        DeliveryMethod = deliveryMethod;
        OrderItems = orderItems;
        Total = GetTotal(subTotal);
    }

    [Required] public decimal SubTotal { get; set; }

    [Required] public string BuyerEmail { get; private set; }
    
    [Required] public ICollection<OrderItemVO> OrderItems { get; private set; }
    
    [Required] public DateTime OrderDate { get; private set; } = DateTime.Now;
    public AddressVO ShippingAddress { get; set; }

    public DeliveryMethod DeliveryMethod { get; private set; }

    [Required] public Guid DeliveryMethodId { get; private set; }

    [Required] public decimal Total { get; private set; }

    [Required] public string UserId { get; private set; }

    [JsonIgnore]
    public User User { get; private set; }

    private void ValidateSubTotal(decimal subTotal)
    {
        DomainValidationException.When(decimal.IsNegative(subTotal), "Total order price should be positive.");
        SubTotal = subTotal;
    }

    private decimal GetTotal(decimal subTotal)
    {
        var total = subTotal + DeliveryMethod.Price;
        if (total < 0) throw new Exception("Order total should be positive.");
        return total;
    }

    public void UpdateOrder(Order order)
    {
        Total = order.Total;
        SubTotal = order.SubTotal;
        ShippingAddress = order.ShippingAddress;
        BuyerEmail = order.BuyerEmail;
        OrderItems = order.OrderItems;
        OrderDate = order.OrderDate;
        DeliveryMethod = order.DeliveryMethod;
        DeliveryMethodId = order.DeliveryMethodId;
        User = order.User;
        UserId = order.UserId;
    }
}