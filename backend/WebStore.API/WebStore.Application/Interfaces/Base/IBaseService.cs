namespace WebStore.API.Interfaces.Base;

public interface IBaseService<T>
{
    Task<IEnumerable<T>> GetAll();
    Task<T> GetById(Guid? id);
    Task<T> Create(T type);
    Task Update(Guid? id, T type);
    Task Delete(Guid? id);
}