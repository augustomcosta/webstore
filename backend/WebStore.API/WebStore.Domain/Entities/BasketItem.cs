namespace WebStore.Domain.Entities;

public sealed class BasketItem
{
    public int Quantity { get; set; }
    public string ProductName { get; set; } = "";
    public string ProductId { get; set; } = "";
    public decimal Price { get; set; }
    
}