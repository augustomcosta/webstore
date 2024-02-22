using Microsoft.EntityFrameworkCore;
using WebStore.Domain.Entities;
using WebStore.Domain.Pagination;
using WebStore.Domain.Repositories;
using WebStore.Infra.Context;

namespace WebStore.Data.RepositoriesImpl;

public class CategoryRepository : ICategoryRepository
{
    private readonly AppDbContext _context;

    public CategoryRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<ProductCategory>> GetAll()
    {
        var categories = await _context.Categories.ToListAsync();
        return categories;
    }

    public async Task<ProductCategory> GetById(Guid? id)
    {
        var productById = await _context.Categories.FirstOrDefaultAsync(p => p.Id == id);
        
        if (productById == null)
        {
            throw new Exception($"Product with id {id} not found");
        }
        
        return productById;
    }

    public async Task<ProductCategory> Create(ProductCategory category)
    {
        _context.Categories.AddAsync(category);
        await _context.SaveChangesAsync();
        return category;
    }

    public async Task<ProductCategory> Update(Guid? id, ProductCategory category)
    {
        var categoryById = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
        
        if (categoryById == null)
        {
            throw new Exception($"Category with id {id} was not found.");
        }
        
        category.GetName(categoryById);
        return categoryById;
    }

    public async Task<ProductCategory> Delete(Guid? id)
    {
        var categoryToDelete = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
        _context.Remove(categoryToDelete);
        await _context.SaveChangesAsync();
        return categoryToDelete;
    }

    public async Task<PagedList<ProductCategory>> GetWithPagination(CategoryParams categoryParams)
    {
        var getCategories = await GetAll();
        var categories = getCategories.OrderBy(c => c.Name).AsQueryable();
        var orderedCategories = PagedList<ProductCategory>
            .ToPagedList(categories,categoryParams.PageNumber,categoryParams.PageSize);
        return orderedCategories;
    }
}