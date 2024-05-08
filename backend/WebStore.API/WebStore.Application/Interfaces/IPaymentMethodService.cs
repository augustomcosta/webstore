namespace WebStore.API.Interfaces;

public interface IPaymentMethodService<T>
{
    Task<List<T>> GetAll();
}