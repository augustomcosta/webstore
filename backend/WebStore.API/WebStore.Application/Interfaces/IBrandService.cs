using WebStore.API.DTOs;
using WebStore.API.Interfaces.Base;
using WebStore.Domain.Pagination;

namespace WebStore.API.Interfaces;

public interface IBrandService : IBaseService<BrandDto>
{
    Task<PagedList<BrandDto>> GetWithPagination(BrandParams brandParams);
}