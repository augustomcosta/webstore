using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebStore.Domain.Entities;
using WebStore.Domain.Entities.Identity;
using WebStore.Domain.Entities.OrderAggregate;

namespace WebStore.Infra.Context;

public sealed class AppDbContext : IdentityDbContext<ApplicationUser>
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Product>? Products { get; set; }
    public DbSet<ProductBrand>? Brands { get; set; }
    public DbSet<ProductCategory>? Categories { get; set; }
    public DbSet<User>? Users { get; set; }
    public DbSet<Order>? Orders { get; set; }
    public DbSet<Basket>? Baskets { get; set; }
    public DbSet<Wishlist>? Wishlists { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}