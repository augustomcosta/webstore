using Microsoft.EntityFrameworkCore;
using WebStore.Domain.Entities;
using WebStore.Domain.Repositories;
using WebStore.Infra.Context;

namespace WebStore.Data.RepositoriesImpl;

public class PaymentMethodRepository : IPaymentMethodRepository<PaymentMethod>
{
    private readonly AppDbContext _context;
    
    public PaymentMethodRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<PaymentMethod>> GetAll()
    {
        var paymentMethods = await _context.PaymentMethods!.ToListAsync();
        if (paymentMethods is null)
        {
            throw new Exception("No payment methods were found");
        }

        return paymentMethods;
    }
}