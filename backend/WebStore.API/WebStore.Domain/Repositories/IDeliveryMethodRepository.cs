using WebStore.Domain.Entities.OrderAggregate;

namespace WebStore.Domain.Repositories;

public interface IDeliveryMethodRepository
{
    Task<IEnumerable<DeliveryMethod>> GetAll();
    Task<DeliveryMethod> GetById(string? id);
    Task<DeliveryMethod> Create(DeliveryMethod deliveryMethod);
    Task<DeliveryMethod> Delete(string? id);
}