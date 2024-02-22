using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;
using WebStore.Domain.Entities.Base;
using WebStore.Domain.Validation;

namespace WebStore.Domain.Entities;

public sealed class Product : BaseEntity
{

    [Required]
    [MinLength(5)]
    [StringLength(30)]
    public string Name { get; private set; } 
    
    [Required]
    [MinLength(5)]
    [StringLength(50)]
    public string Description { get; private set; }
    
    [Required]
    public decimal Price { get; private set; }
    
    [Required]
    [MinLength(5)]
    [StringLength(300)]
    public string ImageUrl { get; private set; }
    
    [Required]
    public Guid  BrandId { get; private set; }
    
    public ProductBrand Brand { get; private set; }
    
    public string? BrandName { get; set; }
    
    [Required]
    public Guid CategoryId { get; private set; }
    
    public ProductCategory Category { get; private set; }
    
    public string? CategoryName { get; set; }


    public Product() {}

    public Product(Guid id, string name, string description, decimal price, string imageUrl,Guid brandId, Guid categoryId) : base(id)
    {
        Validate(name,description,price,imageUrl,brandId,categoryId);
        Brand.Id = BrandId;
    }
    
    public void UpdateProduct(Product product)
    {
        Validate(product.Name, product.Description, product.Price, product.ImageUrl, product.BrandId, product.CategoryId);
    }
    
    private void Validate(string name, string description, decimal price, string imageUrl, Guid brandId, Guid categoryId)
    {
        ValidateName(name);
        ValidateDescription(description);
        ValidatePrice(price);
        ValidateImageUrl(imageUrl);
        ValidateBrandId(brandId);
        ValidateCategoryId(categoryId);
    }

    private void ValidateBrandId(Guid brandId)
    {
        DomainValidationException.When(string.IsNullOrEmpty(brandId.ToString()),"Invalid Guid. Guid is required");
        DomainValidationException.When(string.IsNullOrWhiteSpace(brandId.ToString()),"Invalid Guid. Guid is required");
    }
    
    private void ValidateCategoryId(Guid categoryId)
    {
        DomainValidationException.When(string.IsNullOrEmpty(categoryId.ToString()),"Invalid Guid. Guid is required");
        DomainValidationException.When(string.IsNullOrWhiteSpace(categoryId.ToString()),"Invalid Guid. Guid is required");
    }


    private void ValidateName(string name)
    {
        DomainValidationException.When(string.IsNullOrEmpty(name),"Invalid name. Name is required");
        DomainValidationException.When(string.IsNullOrWhiteSpace(name),"Invalid name. Name is required");
        DomainValidationException.When(name.Length < 5, "Invalid name. Name should have at least 5 characters");
        DomainValidationException.When(name.Length > 30, "Invalid name. Name should a maximum of 30 characters");
        Name = name;
    }

    private void ValidateDescription(string description)
    {
        DomainValidationException.When(string.IsNullOrEmpty(description),"Invalid description. Description is required");
        DomainValidationException.When(string.IsNullOrWhiteSpace(description),"Invalid description. Description is required");
        DomainValidationException.When(description.Length < 5, "Invalid description. Description should have at least 5 characters");
        DomainValidationException.When(description.Length > 50, "Invalid description. Description should a maximum of 50 characters");
        Description = description;
    }

    private void ValidatePrice(decimal price)
    {
        DomainValidationException.When(decimal.IsNegative(price), "Invalid price. Price should be positive");
        Price = price;
    }

    private void ValidateImageUrl(string imageUrl)
    {
        DomainValidationException.When(string.IsNullOrEmpty(imageUrl),"Invalid image url. Image url is required");
        DomainValidationException.When(string.IsNullOrWhiteSpace(imageUrl),"Invalid image url. Image url is required");
        DomainValidationException.When(imageUrl.Length < 5, "Invalid image url. Image url should have at least 5 characters");
        DomainValidationException.When(imageUrl.Length > 300, "Invalid image url. Image url should a maximum of 100 characters");
        ImageUrl = imageUrl;
    }
}