using WebStore.Domain.Entities.OrderAggregate;
using WebStore.Domain.Entities.OrderAggregate.ValueObjects;
using WebStore.Domain.Pagination;
using WebStore.Domain.Repositories.Base;
using WebStore.Domain.ValueObjects;

namespace WebStore.Domain.Repositories;

public interface IOrderRepository : IBaseRepository<Order>
{
    Task<Order> CreateOrder(string basketId, string userId, string deliveryMethodId);
    Task<Order> AddItemToOrder(Guid id, OrderItemVO orderItem);
    Task<IEnumerable<Order>> GetAllOrdersForUser(string userId);
}