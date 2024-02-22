namespace WebStore.Domain.Repositories.Base;

public interface IBaseRepository<T>
{
    Task<IEnumerable<T>> GetAll();
    Task<T> GetById(Guid? id);
    Task<T> Create(T type);
    Task<T> Update(Guid? id, T type);
    Task<T> Delete(Guid? id);
}