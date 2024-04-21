using System.ComponentModel.DataAnnotations.Schema;

namespace WebStore.Domain.Entities;

public sealed class Wishlist
{
    public string? Id { get; set; }
    public string? UserId { get; set; }
    
    [ForeignKey("UserId")]
    public User? User {get; set;}
    public List<Product> Products { get; set; } = [];

    public void UpdateWishlist(Wishlist wishlist)
    {
        wishlist.Id = Id;
        wishlist.UserId = UserId;
        wishlist.Products = Products;
    }
}