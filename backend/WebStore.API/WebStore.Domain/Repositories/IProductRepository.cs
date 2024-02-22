using WebStore.Domain.Entities;
using WebStore.Domain.Pagination;
using WebStore.Domain.Repositories.Base;

namespace WebStore.Domain.Repositories;

public interface IProductRepository : IBaseRepository<Product>
{
    Task<PagedList<Product>> GetWithPagination(ProductParams productParams);
    Task<PagedList<Product>> GetWithPriceFilter(ProductsPriceFilter priceFilter);
    Task<PagedList<Product>> GetProductsByBrandNameAsync(QueryStringParams query, string brandName);
    Task<PagedList<Product>> GetProductsByCategoryNameAsync(QueryStringParams query, string categoryName);
}