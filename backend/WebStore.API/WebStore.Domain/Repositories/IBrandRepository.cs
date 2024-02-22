using WebStore.Domain.Entities;
using WebStore.Domain.Pagination;
using WebStore.Domain.Repositories.Base;

namespace WebStore.Domain.Repositories;

public interface IBrandRepository : IBaseRepository<ProductBrand>
{
    Task<PagedList<ProductBrand>> GetWithPagination(BrandParams brandParams);
}