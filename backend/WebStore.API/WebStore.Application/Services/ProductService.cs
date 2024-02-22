using AutoMapper;
using WebStore.API.DTOs;
using WebStore.API.Interfaces;
using WebStore.Domain.Entities;
using WebStore.Domain.Pagination;
using WebStore.Domain.Repositories;

namespace WebStore.API.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;
    private readonly IMapper _mapper;
    
    public ProductService(IProductRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<ProductDto>> GetAll()
    {
        var productEntities = await _repository.GetAll();
        return _mapper.Map<IEnumerable<ProductDto>>(productEntities);
    }

    public async Task<ProductDto> GetById(Guid? id)
    {
        var productEntity = await _repository.GetById(id);
        return _mapper.Map<ProductDto>(productEntity);
    }

    public async Task<ProductDto> Create(ProductDto productDto)
    {
        var productEntity = _mapper.Map<Product>(productDto);
        await _repository.Create(productEntity);
        return productDto;
    }
    
    public async Task Update(Guid? id, ProductDto productDto)
    {
        var productEntity = _mapper.Map<Product>(productDto);
        await _repository.Update(id, productEntity); 
    }

    public async Task Delete(Guid? id)
    {
        await _repository.Delete(id);
    }

    public async Task<PagedList<ProductDto>> GetWithPagination(ProductParams productParams)
    {
        var productsPaged = await _repository.GetWithPagination(productParams);

        var productsDto = _mapper.Map<IEnumerable<ProductDto>>(productsPaged).ToList();

        var productsDtoPaged = new PagedList<ProductDto>(
            productsDto,
            productsPaged.TotalCount,
            productsPaged.CurrentPage,
            productsPaged.PageSize
        );

        return productsDtoPaged;
    }

    public async Task<PagedList<ProductDto>> GetWithPriceFilter(ProductsPriceFilter priceFilter)
    {
        var products = await _repository.GetWithPriceFilter(priceFilter);
        return _mapper.Map<PagedList<ProductDto>>(products);
    }

    public async Task<PagedList<ProductDto>> GetProductsByBrandNameAsync(QueryStringParams query, string brandName)
    {
        var products = await _repository.GetProductsByBrandNameAsync(query,brandName);
        return _mapper.Map<PagedList<ProductDto>>(products);
    }

    public async Task<PagedList<ProductDto>> GetProductsByCategoryNameAsync(QueryStringParams query, string categoryName)
    {
        var products = await _repository.GetProductsByCategoryNameAsync(query, categoryName);
        return _mapper.Map<PagedList<ProductDto>>(products);
    }
}