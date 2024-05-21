using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;
using WebStore.Domain.Entities.OrderAggregate;
using WebStore.Domain.Entities.OrderAggregate.ValueObjects;
namespace WebStore.Infra.EntityConfiguration;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(o => o.Id);
        builder.HasOne(o => o.User)
            .WithMany(u => u.Orders)
            .HasForeignKey(o => o.UserId);
        builder.HasOne(o => o.DeliveryMethod).WithMany().HasForeignKey(o => o.DeliveryMethodId);
        builder.Property(o => o.Total).IsRequired();
        builder.Property(o => o.BuyerEmail).IsRequired();
        builder.Property(o => o.SubTotal).IsRequired();
        builder.Property(o => o.OrderItems)
            .IsRequired();
        builder.Property(o => o.OrderItems)
            .IsRequired()
            .HasConversion(new ValueConverter<List<OrderItemVO>, string>(
                v => JsonConvert.SerializeObject(v),
                v => JsonConvert.DeserializeObject<List<OrderItemVO>>(v)));
    }
}