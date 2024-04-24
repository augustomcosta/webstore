using WebStore.Domain.Entities.OrderAggregate;

namespace WebStore.Domain.Repositories;

public interface IDeliveryMethodRepository
{
    Task<IEnumerable<DeliveryMethod>> GetAll();
    Task<DeliveryMethod> GetById(Guid? id);
    Task<DeliveryMethod> Create(DeliveryMethod deliveryMethod);
    Task<DeliveryMethod> Delete(Guid? id);
}