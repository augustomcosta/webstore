using AutoMapper;
using WebStore.API.DTOs;
using WebStore.API.Interfaces;
using WebStore.Domain.Entities;
using WebStore.Domain.Pagination;
using WebStore.Domain.Repositories;

namespace WebStore.API.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _repository;
    private readonly IMapper _mapper;
    
    public CategoryService(ICategoryRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CategoryDto>> GetAll()
    {
        var categoryEntites = await _repository.GetAll();
        return _mapper.Map<IEnumerable<CategoryDto>>(categoryEntites);
    }

    public async Task<CategoryDto> GetById(Guid? id)
    {
        var categoryEntity = await _repository.GetById(id);
        return _mapper.Map<CategoryDto>(categoryEntity);
    }

    public async Task<CategoryDto> Create(CategoryDto categoryDto)
    {
        var categoryEntity = _mapper.Map<ProductCategory>(categoryDto);
        await _repository.Create(categoryEntity);
        return categoryDto;
    }

    public async Task Update(Guid? id, CategoryDto categoryDto)
    {
        var categoryEntity = _mapper.Map<ProductCategory>(categoryDto);
        await _repository.Update(id,categoryEntity);
    }

    public async Task Delete(Guid? id)
    {
        await _repository.Delete(id);
    }

    public async Task<PagedList<CategoryDto>> GetWithPagination(CategoryParams categoryParams)
    {
       var categoryEntities = await _repository.GetWithPagination(categoryParams);
       return _mapper.Map<PagedList<CategoryDto>>(categoryEntities);
    }
}