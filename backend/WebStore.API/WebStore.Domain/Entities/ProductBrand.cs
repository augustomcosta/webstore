using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using WebStore.Domain.Entities.Base;
using WebStore.Domain.Validation;

namespace WebStore.Domain.Entities;

public sealed class ProductBrand : BaseEntity
{
    public ProductBrand() {}
    
    public ProductBrand(Guid id, string name,string imageUrl) : base(id)
    {
        ValidateName(name);
        ValidateImageUrl(imageUrl);
        Products = new Collection<Product>();
    }
    
    [Required]
    [MinLength(5)]
    [StringLength(30)]
    public string? Name { get; private set; }
    
    [MinLength(5)]
    [StringLength(300)]
    
    public string? ImageUrl { get; private set; }
    
    public ICollection<Product>? Products { get; private set; }

    private void ValidateName(string name)
    {
        DomainValidationException.When(string.IsNullOrEmpty(name),"Invalid brand name. Name is required");
        DomainValidationException.When(string.IsNullOrWhiteSpace(name),"Invalid brand name. Name is required");
        DomainValidationException.When(name.Length < 5, "Invalid brand name. Name should have at least 5 characters");
        DomainValidationException.When(name.Length > 30, "Invalid brand name. Name should a maximum of 30 characters");
        Name = name;
    }
    
    private void ValidateImageUrl(string imageUrl)
    {
        DomainValidationException.When(string.IsNullOrEmpty(imageUrl),"Invalid brand name. Name is required");
        DomainValidationException.When(string.IsNullOrWhiteSpace(imageUrl),"Invalid brand name. Name is required");
        DomainValidationException.When(imageUrl.Length < 5, "Invalid brand name. Name should have at least 5 characters");
        DomainValidationException.When(imageUrl.Length > 300, "Invalid brand name. Name should a maximum of 30 characters");
        ImageUrl = imageUrl;
    }

    public void UpdateBrand(ProductBrand brand)
    {
        brand.Name = Name;
        brand.ImageUrl = ImageUrl;
    }
}