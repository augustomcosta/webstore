using WebStore.Domain.Entities.Base;

namespace WebStore.Domain.Entities;

public class Basket : BaseEntity
{
    public ICollection<BasketItem> BasketItems { get; set; }
    
    public Basket() { }

    public Basket(Guid id) : base(id)
    {
        
    }
    
    
}