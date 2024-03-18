using WebStore.Domain.Entities.OrderAggregate;
using WebStore.Domain.Entities.OrderAggregate.ValueObjects;
using WebStore.Domain.Repositories.Base;

namespace WebStore.Domain.Repositories;

public interface IOrderRepository : IBaseRepository<Order>
{
    Task<Order> CreateOrder(Guid basketId, string userId);
    Task<Order> AddItemToOrder(Guid id, OrderItemVO orderItem);
}