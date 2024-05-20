using Microsoft.EntityFrameworkCore;
using WebStore.Domain.Entities.OrderAggregate;
using WebStore.Domain.Entities.OrderAggregate.ValueObjects;
using WebStore.Domain.Pagination;
using WebStore.Domain.Repositories;
using WebStore.Infra.Context;

namespace WebStore.Data.RepositoriesImpl;

public class OrderRepository : IOrderRepository
{
    private readonly IBasketRepository _basketRepo;
    private readonly AppDbContext _context;
    private readonly IUserRepository _userRepo;

    public OrderRepository(AppDbContext context, IBasketRepository basketRepo, IUserRepository userRepo)
    {
        _context = context;
        _basketRepo = basketRepo;
        _userRepo = userRepo;
    }

    public async Task<IEnumerable<Order>> GetAll()
    {
        var orders = await _context.Orders!.ToListAsync();
        if (orders == null) throw new Exception("No orders were found.");

        return orders;
    }

    public async Task<Order> GetById(Guid? id)
    {
        var orderById = await _context.Orders!.FirstOrDefaultAsync(o => o.Id == id);
        if (orderById == null) throw new Exception($"Order with Id {id} was not found.");

        return orderById;
    }

    public Task<Order> Create(Order type)
    {
        throw new NotImplementedException();
    }

    public async Task<Order> CreateOrder(string basketId, string userId,string deliveryMethodId)
    {
        var basket = await _basketRepo.GetBasketAsync(basketId);
        var user = await _userRepo.GetById(userId);

        var orderItems = new List<OrderItemVO>();
        foreach (var item in basket.BasketItems)
        {
            var orderItem = new OrderItemVO(item.Quantity, item.Price, item.ProductName, item.Id,
                 item.ProductImgUrl, item.Brand, item.Category);
            orderItems.Add(orderItem);
        }

        var order = new Order(userId, orderItems)
        {
            ShippingAddress = user.Address
        };

        order.DeliveryMethodId = deliveryMethodId;
        var deliveryMethod = await _context.DeliveryMethods.FirstOrDefaultAsync(d => d.Id == deliveryMethodId);
        if (deliveryMethod is null)
        {
            throw new Exception("Delivery method not found while creating Order");
        }
        
        order.BuyerEmail = user.Email;

        foreach (var item in orderItems)
        {
            order.SubTotal += item.Price;
        }

        order.Total = order.SubTotal + deliveryMethod.Price;

        await _context.Orders!.AddAsync(order);

        await _context.SaveChangesAsync();

        return order;
    }

    public async Task<Order> Update(Guid? id, Order order)
    {
        var orderToUpdate = await _context.Orders!.FirstOrDefaultAsync(o => o.Id == id);
        
        if (orderToUpdate is null) throw new Exception($"Order with ID {id} not found");

        order.UpdateOrder(orderToUpdate);

        return order;
    }

    public async Task<Order> Delete(Guid? id)
    {
        var orderToDelete = await _context.Orders!.FirstOrDefaultAsync(o => o.Id == id);
        
        if (orderToDelete is null) throw new Exception($"Order with ID {id} not found");

        _context.Remove(orderToDelete);

        await _context.SaveChangesAsync();

        return orderToDelete;
    }

    public async Task<Order> AddItemToOrder(Guid id, OrderItemVO orderItem)
    {
        var orderSelected = await _context.Orders!.FirstOrDefaultAsync(o => o.Id == id);

        if (orderSelected is null) throw new Exception("Order does not exist");

        orderSelected.OrderItems.Add(orderItem);

        await Update(id, orderSelected);

        await _context.SaveChangesAsync();

        return orderSelected;
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
        if (orders == null) throw new Exception("No orders were found");

        var queryOrders = orders.AsQueryable();

        var filteredOrders = queryOrders.Where(o => o.BuyerEmail == emailFilter.BuyerEmail);

        var paginatedOrders =
            PagedList<Order>.ToPagedList(filteredOrders, emailFilter.PageNumber, emailFilter.PageSize);

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
}