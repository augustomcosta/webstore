using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using WebStore.Domain.Entities;
using WebStore.Domain.Entities.OrderAggregate;

namespace WebStore.Infra.Context;

public class SeedDataContext
{
    public static void SeedData(string brandsData, string categoriesData, string productsData, string paymentMethodsData, string deliveryMethodsData , IServiceProvider serviceProvider)
    {
        var settings = new JsonSerializerSettings
        {
            ContractResolver = new PrivateSetterContractResolver()
        };
        
        var brands = JsonConvert.DeserializeObject<List<ProductBrand>>(brandsData, settings)!;
        
        var categories = JsonConvert.DeserializeObject<List<ProductCategory>>(categoriesData, settings)!;
        
        var products = JsonConvert.DeserializeObject<List<Product>>(productsData, settings)!;
        
        var paymentMethods = JsonConvert.DeserializeObject<List<PaymentMethod>>(paymentMethodsData, settings)!;

        var deliveryMethods = JsonConvert.DeserializeObject<List<DeliveryMethod>>(deliveryMethodsData, settings)!;
        
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
            
            if (!context.PaymentMethods!.Any())
            {
                context.AddRange(paymentMethods);
                context.SaveChanges();
            }
            
            if (!context.DeliveryMethods!.Any())
            {
                context.AddRange(deliveryMethods);
                context.SaveChanges();
            }
        }
    }
}