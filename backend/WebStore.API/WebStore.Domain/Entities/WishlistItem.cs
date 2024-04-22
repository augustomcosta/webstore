namespace WebStore.Domain.Entities;

public class WishlistItem
{
    public string Id { get; set; } = "";
    public string ProductName { get; set; } = "";
    public string ProductImgUrl { get; set; } = "";
    public decimal Price { get; set; }
    public string Brand {get; set;} = "";
    public string Category {get; set;} = "";
}