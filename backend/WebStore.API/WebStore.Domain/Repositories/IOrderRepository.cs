using WebStore.Domain.Entities.OrderAggregate;
using WebStore.Domain.Repositories.Base;

namespace WebStore.Domain.Repositories;

public interface IOrderRepository : IBaseRepository<Order> {}