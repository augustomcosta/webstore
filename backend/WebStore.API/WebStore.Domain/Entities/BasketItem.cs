namespace WebStore.Domain.Entities;

public class BasketItem
{
    public int Quantity { get; set; }
    public string ProductName { get; set; } = "";
    public string ProductId { get; set; } = "";
    public string ProductImgUrl { get; set; } = "";
    public decimal Price { get; set; }
    public string? Brand {get; set;}
    public string? Category {get; set;}
}