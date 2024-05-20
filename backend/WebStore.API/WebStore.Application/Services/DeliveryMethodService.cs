using AutoMapper;
using WebStore.API.DTOs;
using WebStore.API.Interfaces;
using WebStore.Domain.Entities.OrderAggregate;
using WebStore.Domain.Repositories;

namespace WebStore.API.Services;

public class DeliveryMethodService : IDeliveryMethodService<DeliveryMethodDto>
{
    private readonly IDeliveryMethodRepository _repository;
    private readonly IMapper _mapper;

    public DeliveryMethodService(IDeliveryMethodRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<DeliveryMethodDto>> GetAll()
    {
        var deliveryMethods = await _repository.GetAll();
        
        return _mapper.Map<IEnumerable<DeliveryMethodDto>>(deliveryMethods);
    }

    public async Task<DeliveryMethodDto> GetById(string? id)
    {
        var deliveryMethod = await _repository.GetById(id);

        return _mapper.Map<DeliveryMethodDto>(deliveryMethod);
    }

    public async Task<DeliveryMethodDto> Create(DeliveryMethodDto deliveryMethodDto)
    {
       var deliveryMethod = _mapper.Map<DeliveryMethod>(deliveryMethodDto);

       await _repository.Create(deliveryMethod);

       return deliveryMethodDto;

    }

    public async Task Delete(string? id)
    {
        await _repository.Delete(id);
    }
}