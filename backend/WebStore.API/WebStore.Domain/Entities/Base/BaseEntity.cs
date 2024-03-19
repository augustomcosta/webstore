using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebStore.Domain.Entities.Base;

public abstract class BaseEntity 
{
    public BaseEntity() {}
    protected BaseEntity(Guid? id = null)
    {
        Id = id ?? Guid.NewGuid();
    }
    
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [JsonIgnore]
    public Guid Id { get; set; }
    
}