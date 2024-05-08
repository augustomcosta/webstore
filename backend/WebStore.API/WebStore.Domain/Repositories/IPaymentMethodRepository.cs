using WebStore.Domain.Entities;

namespace WebStore.Domain.Repositories;

public interface IPaymentMethodRepository<T>
{
     Task<List<T>> GetAll();
}