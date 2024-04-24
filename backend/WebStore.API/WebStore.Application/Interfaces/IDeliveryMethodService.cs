using WebStore.Domain.Entities.OrderAggregate;

namespace WebStore.API.Interfaces;

public interface IDeliveryMethodService<T>
{
    Task<IEnumerable<T>> GetAll();
    Task<T> GetById(Guid? id);
    Task<T> Create(T deliveryMethod);
    Task Delete(Guid? id);
}