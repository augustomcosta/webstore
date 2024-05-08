using System.Text;
using System.Threading.RateLimiting;
using System.Web.Http;
using Asp.Versioning;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebStore.API.Controllers;
using WebStore.API.DTOs;
using WebStore.API.Interfaces;
using WebStore.API.Mappings;
using WebStore.API.Services;
using WebStore.Data.RepositoriesImpl;
using WebStore.Domain.Entities;
using WebStore.Domain.Entities.Identity;
using WebStore.Domain.Repositories;
using WebStore.Infra.Context;

namespace WebStore.API.DependencyInjection;

public class DependencyInjection
{
    public IServiceCollection AddInfrastructure(
        IServiceCollection services,
        IConfiguration configuration,
        HttpConfiguration config
    )
    {
       
        AddDbContext(services, configuration);
        AddJwtAuthentication(services, configuration);
        AddAuthorization(services);
        AddRepositories(services);
        AddServices(services);
        AddAutoMapper(services);
        AddUserIdentity(services);
        AddCors(services,config);
        AddRequestRateLimit(services);
        AddApiVersioning(services);
        return services;
    }


    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly("WebStore.Application")
            )
        );
    }

    private static void AddApiVersioning(IServiceCollection services)
    {
        services
            .AddApiVersioning(o =>
            {
                o.DefaultApiVersion = new ApiVersion(1, 0);
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.ReportApiVersions = true;
            })
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });
    }

    private static void AddRequestRateLimit(IServiceCollection services)
    {
        services.AddRateLimiter(options =>
        {
            options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
            options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(context =>
                RateLimitPartition.GetSlidingWindowLimiter(
                    context.User.Identity?.Name
                    ?? context.Request.Headers.Host.ToString(),
                    partition => new SlidingWindowRateLimiterOptions
                    {
                        AutoReplenishment = true,
                        PermitLimit = 10,
                        QueueLimit = 0,
                        SegmentsPerWindow = 2,
                        Window = TimeSpan.FromSeconds(3)
                    }
                )
            );
        });
    }
   
    private static void AddCors(IServiceCollection services, HttpConfiguration config)
    {
        services.AddCors(options =>
            options.AddPolicy(
                "AllowClient",
                policy =>
                    policy
                        .AllowAnyOrigin()
                        .WithMethods("GET", "POST","PUT","DELETE")
                        .AllowAnyHeader()
            )
        );
        config.EnableCors();
    }

    private static void AddAuthorization(IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
            options.AddPolicy(
                "SuperAdminOnly",
                policy => policy.RequireRole("Admin").RequireClaim("id", "augusto")
            );
            options.AddPolicy("UserOnly", policy => policy.RequireRole("User"));
            options.AddPolicy(
                "ExclusiveOnly",
                policy =>
                    policy.RequireAssertion(context =>
                        context.User.HasClaim(claim =>
                            claim.Type == "id"
                            || claim.Value == "augusto"
                            || context.User.IsInRole("SuperAdminOnly")
                        )
                    )
            );
        });
    }

    private static void AddJwtAuthentication(
        IServiceCollection services,
        IConfiguration configuration
    )
    {
        var secretKey =
            configuration["JWT:SecretKey"] ?? throw new ArgumentException("Invalid secret key.");

        services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
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
        services
            .AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IBrandRepository, BrandRepository>();
        services.AddScoped<IBasketRepository, BasketRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IWishlistRepository, WishlistRepository>();
        services.AddScoped<IDeliveryMethodRepository, DeliveryMethodRepository>();
        services.AddScoped<IPaymentMethodRepository<PaymentMethod>, PaymentMethodRepository>();
    }

    private static void AddServices(IServiceCollection services)
    {
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IBrandService, BrandService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IBasketService, BasketService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IWishlistService, WishlistService>();
        services.AddScoped<WishlistController>();
        services.AddScoped<IDeliveryMethodService<DeliveryMethodDto>, DeliveryMethodService>();
        services.AddScoped<IPaymentMethodService<PaymentMethodDto>, PaymentMethodService>();
    }

    private static void AddAutoMapper(IServiceCollection services)
    {
        services.AddAutoMapper(typeof(DomainToDtoMappingProfiles));
    }
}