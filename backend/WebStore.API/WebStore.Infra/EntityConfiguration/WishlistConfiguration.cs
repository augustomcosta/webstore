using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using WebStore.Domain.Entities;

namespace WebStore.Infra.EntityConfiguration;

public class WishlistConfiguration : IEntityTypeConfiguration<Wishlist>
{
    public void Configure(EntityTypeBuilder<Wishlist> builder)
    {
        builder.HasKey(w => w.Id);
        builder.Property(w => w.Products).HasConversion(v => JsonConvert.SerializeObject(v),
            v => JsonConvert.DeserializeObject<List<Product>>(v));
        builder.HasOne(w => w.User)
            .WithOne(u => u.Wishlist)
            .HasForeignKey<Wishlist>(w => w.UserId)
            .IsRequired();
    }
}