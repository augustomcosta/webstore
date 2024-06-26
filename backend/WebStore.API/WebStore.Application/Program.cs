using System.Web.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using WebStore.API.DependencyInjection;
using WebStore.Domain.Entities.OrderAggregate.ValueObjects;
using WebStore.Infra.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "webstore-api", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Bearer JWT"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

builder.Services.AddControllers();
var infra = new DependencyInjection();
var httpConfig = new HttpConfiguration();
infra.AddInfrastructure(builder.Services, builder.Configuration,httpConfig);
var app = builder.Build();
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowClient");
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "products")),
    RequestPath = "/images/products"
});

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "categories")),
    RequestPath = "/images/categories"
});

var brandsJson = File.ReadAllText("./SeedDataFiles/brands.json");
var categoriesJson = File.ReadAllText("./SeedDataFiles/categories.json");
var productsJson = File.ReadAllText("./SeedDataFiles/products.json");
var paymentMethodsJson = File.ReadAllText("./SeedDataFiles/payment-methods.json");
var deliveryMethodsJson = File.ReadAllText("./SeedDataFiles/delivery-methods.json");
SeedDataContext.SeedData(brandsJson, categoriesJson, productsJson, paymentMethodsJson, deliveryMethodsJson , app.Services);

app.UseRateLimiter();
app.UseHttpsRedirection();
app.MapControllers();

app.Run();