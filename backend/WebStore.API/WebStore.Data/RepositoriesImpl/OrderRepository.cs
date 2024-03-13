using Microsoft.EntityFrameworkCore;
using WebStore.Domain.Entities.OrderAggregate;
using WebStore.Domain.Entities.OrderAggregate.ValueObjects;
using WebStore.Domain.Pagination;
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

    public async Task<Order> Update(Guid? id, Order order)
    {
        var orderToUpdate = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
        order.UpdateOrder(orderToUpdate);
        return order;
    }

    public async Task<Order> Delete(Guid? id)
    {
        var orderToDelete = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
        _context.Remove(orderToDelete);
        await _context.SaveChangesAsync();
        return orderToDelete;
    }

    public async Task<IEnumerable<Order>> GetWithPagination(OrderParams orderParams)
    {
        var orders = await GetAll();
        var queryOrders = orders.AsQueryable().OrderBy(o => o.Id);
        var paginatedOrders = PagedList<Order>.ToPagedList(queryOrders, orderParams.PageNumber, orderParams.PageSize);
        return paginatedOrders;
    }

    public async Task<IEnumerable<Order>> GetByBuyerEmail(OrdersEmailFilter emailFilter)
    {
        var orders = await GetAll();
        if (orders == null)
        {
            throw new Exception("No orders were found");
        }
        var queryOrders = orders.AsQueryable();
        var filteredOrders = queryOrders.Where(o => o.BuyerEmail == emailFilter.BuyerEmail);
        var paginatedOrders = PagedList<Order>.ToPagedList(filteredOrders, emailFilter.PageNumber, emailFilter.PageSize);
        return paginatedOrders;
    }
    
    

    public async Task<IEnumerable<Order>> GetByOrderDate(OrdersDateFilter dateFilter)
    {
        var orders = await GetAll();
        var queryOrders = orders.AsQueryable();
        var filteredOrders = queryOrders.Where(o => o.OrderDate == dateFilter.OrderDate);
        var paginatedOrders = PagedList<Order>.ToPagedList(filteredOrders, dateFilter.PageNumber, dateFilter.PageSize);
        return paginatedOrders;
    }

    public async Task<Order> AddItemToOrder(Guid id, Order order, OrderItemVO orderItem)
    {
        var orderSelected = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
        if (orderSelected is null)
        {
            throw new Exception("Order does not exist");
        }
        
        orderSelected!.OrderItems.Add(orderItem);
        order.UpdateOrder(orderSelected);
        return order;
    }
}