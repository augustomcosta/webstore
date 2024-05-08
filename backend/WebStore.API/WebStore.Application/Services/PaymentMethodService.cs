using AutoMapper;
using WebStore.API.DTOs;
using WebStore.API.Interfaces;
using WebStore.Domain.Entities;
using WebStore.Domain.Repositories;

namespace WebStore.API.Services;

public class PaymentMethodService : IPaymentMethodService<PaymentMethodDto>
{
    private readonly IPaymentMethodRepository<PaymentMethod> _repository;
    private readonly IMapper _mapper;
    
    public PaymentMethodService(IPaymentMethodRepository<PaymentMethod> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<List<PaymentMethodDto>> GetAll()
    {
        var paymentMethods = await _repository.GetAll();
        
        return _mapper.Map<List<PaymentMethodDto>>(paymentMethods);
    }
}