using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using WebStore.Domain.Entities;
using WebStore.Domain.ValueObjects;

namespace WebStore.Infra.EntityConfiguration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(t => t.Id);
        builder.Property(u => u.FirstName).HasMaxLength(50).IsRequired();
        builder.Property(u => u.LastName).HasMaxLength(50).IsRequired();
        builder.Property(u => u.Username).HasMaxLength(20).IsRequired();
        builder.Property(u => u.Password).HasMaxLength(200).IsRequired();
        builder.Property(u => u.Email).HasMaxLength(100).IsRequired();
        builder.OwnsOne(user => user.Address,
            navigationBuilder =>
            {
                navigationBuilder.Property(address => address.City)
                    .HasColumnName("City");
                navigationBuilder.Property(address => address.State)
                    .HasColumnName("State");
                navigationBuilder.Property(address => address.Street)
                    .HasColumnName("Street");
                navigationBuilder.Property(address => address.Neighborhood)
                    .HasColumnName("Neighborhood");
                navigationBuilder.Property(address => address.Number)
                    .HasColumnName("Number");
                navigationBuilder.Property(address => address.ZipCode)
                    .HasColumnName("ZipCode");
            });
        builder.Property(u => u.Cpf).HasMaxLength(11);
        builder.HasOne(u => u.Basket).WithOne(b => b.User).HasForeignKey<Basket>(b => b.UserId);
    }
}
