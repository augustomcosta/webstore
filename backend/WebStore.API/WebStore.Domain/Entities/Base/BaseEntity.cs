using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using WebStore.Domain.Validation;

namespace WebStore.Domain.Entities.Base;

public abstract class BaseEntity 
{
    public BaseEntity() {}
    protected BaseEntity(Guid id)
    {
        ValidateId(id);
    }
    
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [JsonIgnore]
    public Guid Id { get; set; }

    private void ValidateId(Guid id)
    {
        DomainValidationException.When(string.IsNullOrEmpty(id.ToString()),"Invalid Guid. Guid is required");
        DomainValidationException.When(string.IsNullOrWhiteSpace(id.ToString()),"Invalid Guid. Guid is required");
        Id = id;
    }
}