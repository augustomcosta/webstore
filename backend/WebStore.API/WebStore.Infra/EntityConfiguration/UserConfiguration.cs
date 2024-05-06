﻿using Microsoft.EntityFrameworkCore;
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
        builder.OwnsOne(u => u.Address);
        builder.Property(u => u.Cpf).HasMaxLength(11);
        builder.HasOne(u => u.Basket).WithOne(b => b.User).HasForeignKey<Basket>(b => b.UserId);
    }
}
