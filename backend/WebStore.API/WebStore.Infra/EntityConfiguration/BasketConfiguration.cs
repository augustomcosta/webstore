using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using WebStore.Domain.Entities;
namespace WebStore.Infra.EntityConfiguration;

public class BasketConfiguration : IEntityTypeConfiguration<Basket>
{
    public void Configure(EntityTypeBuilder<Basket> builder)
    {
        builder.HasKey(b => b.Id);
         builder.HasOne(b => b.User)
            .WithOne(u => u.Basket)
            .HasForeignKey<Basket>(b => b.UserId)  
            .IsRequired();
        builder.HasOne(b => b.DeliveryMethod).WithMany().HasForeignKey(b => b.DeliveryMethodId);
        builder.Property(b => b.ShippingPrice);
        builder.Property(b => b.CreatedAt);
        builder.Property(b => b.TotalPrice).IsRequired();
        builder.Property(b => b.BasketItems)
            .IsRequired()
            .HasConversion(
                v => JsonConvert.SerializeObject(v),
                v => JsonConvert.DeserializeObject<List<BasketItem>>(v)
            );
    }
}