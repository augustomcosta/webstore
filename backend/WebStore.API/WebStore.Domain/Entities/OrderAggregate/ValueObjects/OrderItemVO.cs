using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using WebStore.Domain.Validation;

namespace WebStore.Domain.Entities.OrderAggregate.ValueObjects;

[Keyless]
[ComplexType]
public class OrderItemVO
{
    public int Quantity { get; private set; }
    public decimal Price { get; private set; }
    public string ProductName { get; private set; } = "";
    public string ProductId { get; private set; } = "";
    public string ProductImgUrl { get; set; } = "";
    public string? Brand {get; set;}
    public string? Category {get; set;}

    public OrderItemVO() { }

    public OrderItemVO(int quantity, decimal price, string productName, string productId, string productImgUrl, string brand, string category)
    {
        ValidateOrderItem(quantity,price);
        ProductName = productName;
        ProductId = productId;
        ProductImgUrl = productImgUrl;
        Brand = brand;
        Category = category;
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