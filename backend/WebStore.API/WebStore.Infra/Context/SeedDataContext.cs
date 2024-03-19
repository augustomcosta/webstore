using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using WebStore.Domain.Entities;

namespace WebStore.Infra.Context;

public class SeedDataContext
{
    public static void SeedData(string brandsData, string categoriesData, string productsData, IServiceProvider serviceProvider)
    {
        var settings = new JsonSerializerSettings
        {
            ContractResolver = new PrivateSetterContractResolver()
        };
        
        List<ProductBrand> brands = JsonConvert.DeserializeObject<List<ProductBrand>>(brandsData, settings)!;
        
        List<ProductCategory> categories = JsonConvert.DeserializeObject<List<ProductCategory>>(categoriesData, settings)!;
        
        List<Product> products = JsonConvert.DeserializeObject<List<Product>>(productsData, settings)!;
        
        using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
        {
            var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
            if (!context.Brands!.Any())
            {
                context.AddRange(brands);
                context.SaveChanges();
            }

            if (!context.Categories!.Any())
            {
                context.AddRange(categories);
                context.SaveChanges();
            }
            
            if (!context.Products!.Any())
            {
                context.AddRange(products);
                context.SaveChanges();
            }
        }
    }
}