using Microsoft.EntityFrameworkCore;
using WebStore.Domain.Entities.OrderAggregate;
using WebStore.Domain.Repositories;
using WebStore.Infra.Context;

namespace WebStore.Data.RepositoriesImpl;

public class DeliveryMethodRepository : IDeliveryMethodRepository
{
    private readonly AppDbContext _context;
    public DeliveryMethodRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<DeliveryMethod>> GetAll()
    {
        var deliveryMethods = await _context.DeliveryMethods!.ToListAsync();
        if (deliveryMethods is null)
        {
            throw new Exception("No delivery methods were found");
        }

        return deliveryMethods;
    }

    public async Task<DeliveryMethod> GetById(Guid? id)
    {
        var deliveryMethod = await _context.DeliveryMethods!.FirstOrDefaultAsync(d => d.Id == id);
        if (deliveryMethod is null)
        {
            throw new Exception($"Delivery Method with Id {id} was not found");
        }

        return deliveryMethod;
    }


    public async Task<DeliveryMethod> Create(DeliveryMethod deliveryMethod)
    {
        await _context.DeliveryMethods!.AddAsync(deliveryMethod);

        await _context.SaveChangesAsync();

        return deliveryMethod;
    }
    
    public async Task<DeliveryMethod> Delete(Guid? id)
    {
       var deliveryMethod = await _context.DeliveryMethods!.FirstOrDefaultAsync(d => d.Id == id);
       if (deliveryMethod is null)
       {
           throw new Exception($"Delivery method with Id {id} was not found");
       }
       
       _context.DeliveryMethods!.Remove(deliveryMethod);
       

       await _context.SaveChangesAsync();

       return deliveryMethod;
    }
}