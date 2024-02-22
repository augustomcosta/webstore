using WebStore.Domain.Entities;
using WebStore.Domain.Pagination;
using WebStore.Domain.Repositories.Base;

namespace WebStore.Domain.Repositories;

public interface ICategoryRepository : IBaseRepository<ProductCategory>
{
    Task<PagedList<ProductCategory>> GetWithPagination(CategoryParams categoryParams);
}