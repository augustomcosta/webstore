using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebStore.API.Interfaces;
using WebStore.API.Mappings;
using WebStore.API.Services;
using WebStore.Data.RepositoriesImpl;
using WebStore.Domain.Entities.Identity;
using WebStore.Domain.Repositories;
using WebStore.Infra.Context;
using WebStore.IoC.Interfaces;
namespace WebStore.API.DependencyInjection;


public class DependencyInjection : IDependencyInjection
    {
        public IServiceCollection AddInfrastructure(IServiceCollection services, IConfiguration configuration)
        {
            AddDbContext(services, configuration);
            AddJwtAuthentication(services,configuration);
            AddAuthorization(services);
            AddRepositories(services);
            AddServices(services);
            AddAutoMapper(services);
            AddUserIdentity(services);
            return services;
        }

        private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options => options.UseNpgsql(
                configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly("WebStore.Application")
            ));
        }

        private static void AddAuthorization(IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
                options.AddPolicy("SuperAdminOnly", policy => policy.RequireRole("Admin").RequireClaim("id","augusto"));
                options.AddPolicy("UserOnly", policy => policy.RequireRole("User"));
                options.AddPolicy("ExclusiveOnly", policy => policy.RequireAssertion(context => 
                    context.User.HasClaim(claim => claim.Type == "id" || claim.Value == "augusto" 
                                                                      || context.User.IsInRole("SuperAdminOnly"))));
            });
        }

        private static void AddJwtAuthentication(IServiceCollection services, IConfiguration configuration)
        {
            var secretKey = configuration["JWT:SecretKey"] ?? throw new ArgumentException("Invalid secret key.");

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidAudience = configuration["JWT:ValidAudience"],
                    ValidIssuer = configuration["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                };
            });
        }
        
        private static void AddUserIdentity(IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
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
            services.AddScoped<ITokenService, TokenService>();
        }

        private static void AddAutoMapper(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(DomainToDtoMappingProfiles));
        }              
}
