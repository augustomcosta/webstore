using System.Collections.ObjectModel;
using WebStore.Domain.Entities.Base;
using WebStore.Domain.Validation;

namespace WebStore.Domain.Entities;

public sealed class ProductCategory : BaseEntity
{
    public ProductCategory() {}
    public ProductCategory(Guid id, string name) : base(id)
    {
        ValidateName(name);
        Products = new Collection<Product>();
    }

    public string? Name { get; private set; }
    
    public ICollection<Product>? Products { get; private set; }

    private void ValidateName(string name)
    {
        DomainValidationException.When(string.IsNullOrEmpty(name),"Invalid category name. Name is required");
        DomainValidationException.When(string.IsNullOrWhiteSpace(name),"Invalid category name. Name is required");
        DomainValidationException.When(name.Length < 5, "Invalid category name. Name should have at least 5 characters");
        DomainValidationException.When(name.Length > 30, "Invalid category name. Name should a maximum of 30 characters");
        Name = name;
    }

    public void GetName(ProductCategory category)
    {
        category.Name = Name;
    }
    
}