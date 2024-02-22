using Microsoft.EntityFrameworkCore;
using WebStore.Domain.Entities;
using WebStore.Domain.Pagination;
using WebStore.Domain.Repositories;
using WebStore.Infra.Context;

namespace WebStore.Data.RepositoriesImpl;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> GetAll()
    {
        var products = await _context.Products.ToListAsync();
        return products;
    }

    public async Task<Product> GetById(Guid? id)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
        return product;
    }

    public async Task<Product> Create(Product product)
    {
        var brand = await _context.Brands.FirstOrDefaultAsync(b => b.Id == product.BrandId);
        product.BrandName = brand?.Name;
        var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == product.CategoryId);
        product.CategoryName = category?.Name;
        
        _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task<Product> Update(Guid? id, Product product)
    {
        var productById = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
        if (productById == null)
        {
            throw new Exception($"Product with id: {id} not found.");
        }

        product.UpdateProduct(productById);
        return productById;
    }

    public async Task<Product> Delete(Guid? id)
    {
        var productToDelete = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
        _context.Products.Remove(productToDelete);
        await _context.SaveChangesAsync();
        return productToDelete;
    }

    public async Task<PagedList<Product>> GetWithPagination(ProductParams productParams)
    {
        var products = await GetAll();
        var queryableProducts = products.OrderBy(p => p.Id).AsQueryable();
        var orderedProducts = PagedList<Product>.ToPagedList(
            queryableProducts,
            productParams.PageNumber,
            productParams.PageSize
        );
        return orderedProducts;
    }

    public async Task<PagedList<Product>> GetWithPriceFilter(ProductsPriceFilter priceFilter)
    {
        var getProducts = await GetAll();
        var products = getProducts.AsQueryable();

        if (priceFilter.Price.HasValue && !string.IsNullOrEmpty(priceFilter.PriceCriteria))
        {
            if (priceFilter.PriceCriteria.Equals("greater", StringComparison.OrdinalIgnoreCase))
            {
                products = products
                    .Where(p => p.Price > priceFilter.Price.Value)
                    .OrderBy(p => p.Price);
            }

            if (priceFilter.PriceCriteria.Equals("smaller", StringComparison.OrdinalIgnoreCase))
            {
                products = products
                    .Where(p => p.Price < priceFilter.Price.Value)
                    .OrderBy(p => p.Price);
            }

            if (priceFilter.PriceCriteria.Equals("equal", StringComparison.OrdinalIgnoreCase))
            {
                products = products
                    .Where(p => p.Price == priceFilter.Price.Value)
                    .OrderBy(p => p.Price);
            }
        }

        var filteredProducts = PagedList<Product>.ToPagedList(
            products,
            priceFilter.PageNumber,
            priceFilter.PageSize
        );

        return filteredProducts;
    }

    public async Task<Guid> GetBrandIdByName(string? brandName)
    {
        var brand = await _context.Brands.FirstOrDefaultAsync(b => b.Name == brandName);
        if (brand == null)
        {
            throw new Exception($"Brand with name: {brand.Name} not found.");
        }
        return brand.Id;
    }
    
    public async Task<Guid> GetCategoryIdByName(string? categoryName)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(b => b.Name == categoryName);
        if (category == null)
        {
            throw new Exception($"Category with name: {category.Name} not found.");
        }
        return category.Id;
    }

    public async Task<PagedList<Product>> GetProductsByBrandNameAsync(QueryStringParams query, string brandName)
    {
        var brandId = await GetBrandIdByName(brandName);
        var getProducts = await _context.Products
            .Include(p => p.Brand)
            .Where(p => p.Brand.Id == brandId)
            .ToListAsync();

        var products = getProducts.AsQueryable();
        var filteredProducts = PagedList<Product>.ToPagedList(
            products,
            query.PageNumber,
            query.PageSize
        );
        
        return filteredProducts;
    }

    public async Task<PagedList<Product>> GetProductsByCategoryNameAsync(QueryStringParams query, string categoryName)
    {
        var categoryId = await GetCategoryIdByName(categoryName);
        var getProducts = await _context.Products
            .Include(p => p.Category)
            .Where(p => p.Category.Id == categoryId)
            .ToListAsync();

        var products = getProducts.AsQueryable();
        var filteredProducts = PagedList<Product>.ToPagedList(
            products,
            query.PageNumber,
            query.PageSize
        );

        return filteredProducts;
    }
}
