using Microsoft.EntityFrameworkCore;
using WebStore.API.Interfaces;
using WebStore.API.Mappings;
using WebStore.API.Services;
using WebStore.Data.RepositoriesImpl;
using WebStore.Domain.Repositories;
using WebStore.Infra.Context;
using WebStore.IoC.Interfaces;

namespace WebStore.API.DependencyInjection;


public class DependencyInjection : IDependencyInjection
    {
        public IServiceCollection AddInfrastructure(IServiceCollection services, IConfiguration configuration)
        {
            AddDbContext(services, configuration);
            AddRepositories(services);
            AddServices(services);
            AddAutoMapper(services);
            return services;
        }

        private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options => options.UseNpgsql(
                configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly("WebStore.Application")
            ));
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IBrandRepository, BrandRepository>();
        }

        private static void AddServices(IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IBrandService, BrandService>();
            services.AddScoped<ICategoryService, CategoryService>();
        }

        private static void AddAutoMapper(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(DomainToDtoMappingProfiles));
        }              
}
