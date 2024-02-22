using WebStore.API.DTOs;
using WebStore.API.Interfaces.Base;
using WebStore.Domain.Pagination;

namespace WebStore.API.Interfaces;

public interface ICategoryService : IBaseService<CategoryDto>
{
    Task<PagedList<CategoryDto>> GetWithPagination(CategoryParams categoryParams);
}