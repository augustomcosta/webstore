using Microsoft.EntityFrameworkCore;
using WebStore.Domain.Entities.OrderAggregate;
using WebStore.Domain.Repositories;
using WebStore.Infra.Context;

namespace WebStore.Data.RepositoriesImpl;

public class OrderRepository : IOrderRepository
{
    private readonly AppDbContext _context;

    public OrderRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Order>> GetAll()
    {
        var orders = await _context.Orders.ToListAsync();
        if (orders == null)
        {
            throw new Exception("No orders were found.");
        }
        return orders;
    }

    public async Task<Order> GetById(Guid? id)
    {
        var orderById = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
        if (orderById == null)
        {
            throw new Exception($"Order with Id {id} was not found.");
        }

        return orderById;
    }

    public async Task<Order> Create(Order order)
    {
        await _context.Orders.AddAsync(order);
        await _context.SaveChangesAsync();
        return order;
    }

    public Task<Order> Update(Guid? id, Order order)
    {
        throw new NotImplementedException();
    }

    public Task<Order> Delete(Guid? id)
    {
        throw new NotImplementedException();
    }
}