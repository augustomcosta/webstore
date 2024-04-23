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

    public Task<DeliveryMethod> GetById(Guid? id)
    {
        throw new NotImplementedException();
    }


    public async Task<DeliveryMethod> Create(DeliveryMethod deliveryMethod)
    {
        await _context.DeliveryMethods!.AddAsync(deliveryMethod);

        await _context.SaveChangesAsync();

        return deliveryMethod;
    }
    
    public Task<DeliveryMethod> Delete(Guid? id)
    {
        throw new NotImplementedException();
    }
}