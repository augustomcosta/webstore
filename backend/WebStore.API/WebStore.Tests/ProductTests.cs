using FluentAssertions;
using WebStore.Domain.Entities;
using WebStore.Domain.Validation;

namespace WebStore.Tests;

public class ProductTests
{
    [Fact]
    public void ProductConstructorShouldCreateValidProduct()
    {
        // Arrange 
        var sut = () =>
        {
            return new Product(Guid.NewGuid(), "teste", "teste teste", 50, "teste.jpg", Guid.NewGuid(), Guid.NewGuid());
        };
        
        // Assert
        sut.Should().NotThrow<DomainValidationException>();
    }
}