using WebStore.Domain.Entities.OrderAggregate;
using WebStore.Domain.Repositories.Base;

namespace WebStore.Domain.Repositories;

public interface IDeliveryMethodRepository
{
    Task<IEnumerable<DeliveryMethod>> GetAll();
    Task<DeliveryMethod> GetById(Guid? id);
    Task<DeliveryMethod> Create(DeliveryMethod type);
    Task<DeliveryMethod> Delete(Guid? id);
}