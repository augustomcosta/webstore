using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using WebStore.Domain.Validation;

namespace WebStore.Domain.ValueObjects;

[Keyless]
[ComplexType]
public class AddressVO
{
    [Required]
    [MinLength(3)]
    [StringLength(100)]
    public string Street { get;  set; } = "";
    
    [Required]
    [MinLength(3)]
    [StringLength(100)]
    public string Neighborhood { get;  set; } = "";
    
    [Required]
    [MinLength(3)]
    [StringLength(100)]
    public string City { get;  set; } = "";
    
    [Required]
    [MinLength(2)]
    [StringLength(100)]
    public string State { get;  set; } = "";
    
    [Required]
    public string ZipCode { get;  set; } = "";
    
    [Required]
    [MinLength(1)]
    [StringLength(10)]
    public string Number { get;  set; } = "";
    
    public AddressVO() {}

    public AddressVO(string street, string neighborhood, string city, string state, string zipCode, string number)
    {
        ValidateAddress(street,neighborhood,city,state,zipCode,number);
    }

    private void ValidateAddress(string street, string neighborhood, string city, string state, string zipCode, string number)
    {
        ValidateZipCode(zipCode);
        ValidateState(state);
        ValidateCity(city);
        ValidateNeighborhood(neighborhood);
        ValidateStreet(street);
        ValidateNumber(number);
    }
    
    
    private void ValidateZipCode(string zipCode)
    {
        DomainValidationException.When(string.IsNullOrEmpty(zipCode),"Zip Code is required");
        DomainValidationException.When(string.IsNullOrWhiteSpace(zipCode),"Zip Code is required");
        ZipCode = zipCode;
    }
    
    private void ValidateState(string state)
    {
        DomainValidationException.When(string.IsNullOrEmpty(state),"State is required");
        DomainValidationException.When(string.IsNullOrWhiteSpace(state),"State is required");
        DomainValidationException.When(state.Length < 2,"Invalid state. State should have at least 3 characters");
        DomainValidationException.When(state.Length > 100,"Invalid state. State should have a maximum of 100 characters");
        State = state;
    }
    
    private void ValidateCity(string city)
    {
        DomainValidationException.When(string.IsNullOrEmpty(city),"City is required");
        DomainValidationException.When(string.IsNullOrWhiteSpace(city),"City is required");
        DomainValidationException.When(city.Length < 3,"Invalid neighborhood. City should have at least 3 characters");
        DomainValidationException.When(city.Length > 100,"Invalid neighborhood. City should have a maximum of 100 characters");
        City = city;
    }
    
    private void ValidateNeighborhood(string neighborhood)
    {
        DomainValidationException.When(string.IsNullOrEmpty(neighborhood),"Neighborhood is required");
        DomainValidationException.When(string.IsNullOrWhiteSpace(neighborhood),"Neighborhood is required");
        DomainValidationException.When(neighborhood.Length < 3,"Invalid neighborhood. Neighborhood should have at least 3 characters");
        DomainValidationException.When(neighborhood.Length > 100,"Invalid neighborhood. Neighborhood should have a maximum of 100 characters");
        Neighborhood = neighborhood;
    }

    private void ValidateStreet(string street)
    {
        DomainValidationException.When(string.IsNullOrEmpty(street),"Street is required");
        DomainValidationException.When(string.IsNullOrWhiteSpace(street),"Street is required");
        DomainValidationException.When(street.Length < 3,"Invalid street. Street should have at least 3 characters");
        DomainValidationException.When(street.Length > 100,"Invalid street. Street should have a maximum of 100 characters");
        Street = street;
    }

    private void ValidateNumber(string number)
    {
        DomainValidationException.When(string.IsNullOrEmpty(number),"Number is required");
        DomainValidationException.When(string.IsNullOrWhiteSpace(number),"Number is required");
        DomainValidationException.When(number.Length < 1,"Invalid number. Number should have at least 1 character");
        DomainValidationException.When(number.Length > 10,"Invalid number. Number should have a maximum of 10 characters");
        Number = number;
    }
    
}