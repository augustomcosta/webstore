using AutoMapper;
using WebStore.API.DTOs;
using WebStore.API.Interfaces;
using WebStore.Domain.Entities;
using WebStore.Domain.Pagination;
using WebStore.Domain.Repositories;
namespace WebStore.API.Services;

public class BrandService : IBrandService
{
    private readonly IBrandRepository _repository;
    private readonly IMapper _mapper;
    
    public BrandService(IBrandRepository repository, IMapper mapper)
    {
        _repository = repository;
        
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<BrandDto>> GetAll()
    {
        var brandEntities = await _repository.GetAll();
        return _mapper.Map<IEnumerable<BrandDto>>(brandEntities);
    }

    public async Task<BrandDto> GetById(Guid? id)
    {
        var brandEntity = await _repository.GetById(id);
        return _mapper.Map<BrandDto>(brandEntity);
    }

    public async Task<BrandDto> Create(BrandDto brandDto)
    {
        var brandEntity = _mapper.Map<ProductBrand>(brandDto);
        await _repository.Create(brandEntity);
        return brandDto;
    }

    public async Task Update(Guid? id, BrandDto brandDto)
    {
        var brandEntity = _mapper.Map<ProductBrand>(brandDto);
        await _repository.Update(id, brandEntity);
    }

    public async Task Delete(Guid? id)
    {
        await _repository.Delete(id);
    }

    public async Task<PagedList<BrandDto>> GetWithPagination(BrandParams brandParams)
    {
        var brandEntities = await _repository.GetWithPagination(brandParams);
        return _mapper.Map<PagedList<BrandDto>>(brandEntities);
    }
}