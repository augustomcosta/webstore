using WebStore.Domain.Validation;

namespace WebStore.Domain.Entities.OrderAggregate.ValueObjects;

public class OrderItemVO
{
    public int Quantity { get; private set; }
    
    public decimal Price { get; private set; }

    public string ProductName { get; private set; } = "";
    

    public OrderItemVO(int quantity, decimal price)
    {
        ValidateOrderItem(quantity,price);
    }

    private void ValidateOrderItem(int quantity, decimal price)
    {
        ValidateQuantity(quantity);
        ValidatePrice(price);
    }

    
    private void ValidateQuantity(int quantity)
    {
        DomainValidationException.When(int.IsNegative(quantity),"Item quantity should be positive.");
        Quantity = quantity;
    }

    private void ValidatePrice(decimal price)
    {
        DomainValidationException.When(decimal.IsNegative(price),"Item price should be positive");
        Price = price;
    }
}