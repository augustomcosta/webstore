using System.ComponentModel.DataAnnotations;
using WebStore.Domain.Validation;

namespace WebStore.Domain.ValueObjects;

public class AddressVO
{
    [Required]
    [MinLength(3)]
    [StringLength(100)]
    public string Street { get; private set; } = "";
    
    [Required]
    [MinLength(3)]
    [StringLength(100)]
    public string Neighborhood { get; private set; } = "";
    
    [Required]
    [MinLength(3)]
    [StringLength(100)]
    public string City { get; private set; } = "";
    
    [Required]
    [MinLength(3)]
    [StringLength(100)]
    public string State { get; private set; } = "";
    
    [Required]
    public string ZipCode { get; private set; } = "";
    
    [Required]
    [MinLength(1)]
    [StringLength(10)]
    public string Number { get; private set; } = "";
    
    [Required]
    [MinLength(3)]
    [StringLength(100)]
    public string Country { get; private set; } = "";
    
    public AddressVO() {}

    public AddressVO(string street, string neighborhood, string city, string state, string zipCode, string number, string country)
    {
        ValidateAddress(street,neighborhood,city,state,zipCode,number,country);
    }

    private void ValidateAddress(string street, string neighborhood, string city, string state, string zipCode, string number, string country)
    {
        ValidateCountry(country);
        ValidateZipCode(zipCode);
        ValidateState(state);
        ValidateCity(city);
        ValidateNeighborhood(neighborhood);
        ValidateStreet(street);
        ValidateNumber(number);
    }
    
    private void ValidateCountry(string country)
    {
        DomainValidationException.When(string.IsNullOrEmpty(country),"Country is required");
        DomainValidationException.When(string.IsNullOrWhiteSpace(country),"Country is required");
        DomainValidationException.When(country.Length < 3,"Invalid country. Country should have at least 3 characters");
        DomainValidationException.When(country.Length > 100,"Invalid country. Country should have a maximum of 100 characters");
        Country = country;
    }
    
    private void ValidateZipCode(string zipCode)
    {
        DomainValidationException.When(string.IsNullOrEmpty(zipCode),"Zip Code is required");
        DomainValidationException.When(string.IsNullOrWhiteSpace(zipCode),"Zip Code is required");

        switch (Country)
        {
            case "Brasil": 
                DomainValidationException.When(zipCode.Length !=  8,"Invalid zip code. Zip code should have 8 characters");
                ZipCode = zipCode;
                break;
            case "USA": 
                DomainValidationException.When(zipCode.Length !=  5,"Invalid zip code. Zip code should have 5 characters");
                ZipCode = zipCode;
                break;
            case "Trinidad and Tobago":
                DomainValidationException.When(zipCode.Length !=  6,"Invalid zip code. Zip code should have 6 characters");
                ZipCode = zipCode;
                break;
            default:
                throw new DomainValidationException("Invalid Zip Code was provided.");
        }
    }
    
    private void ValidateState(string state)
    {
        DomainValidationException.When(string.IsNullOrEmpty(state),"State is required");
        DomainValidationException.When(string.IsNullOrWhiteSpace(state),"State is required");
        DomainValidationException.When(state.Length < 3,"Invalid state. State should have at least 3 characters");
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