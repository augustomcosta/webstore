using WebStore.Domain.ValueObjects;

namespace WebStore.API.DTOs.UserDto;

public record UpdateUserAddressDto()
{
    public AddressVO Address { get; set; } = new AddressVO();
}