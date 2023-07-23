using Microsoft.EntityFrameworkCore;

namespace Antique_Store_API.Models;

public class DbOperation : IDbOperation
{
    private readonly MainContext _context;

    public DbOperation(MainContext context)
    {
        _context = context;
    }

    public async Task<List<Product>> GetAllProducts()
    {
        var products = await _context.Products.ToListAsync();
        return products;
    }

    public async Task<Product?> GetProductById(int id)
    {
        var products = await _context.Products.FindAsync(id);
        return products ?? null;
    }

    public async Task<List<Product>?> GetProductsByTag(string tag)
    {
        var products = await _context.Products.Where(p => p.tag == tag).ToListAsync();
        return products ?? null;
    }

    public async Task<List<Product>?> GetProductByName(string name)
    {
        var product = await _context.Products.Where(p => p.Name.StartsWith(name) || p.Name.EndsWith(name)).ToListAsync();
        return product ?? null;
    }

    public async Task<List<Product>> AddProduct(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return await _context.Products.ToListAsync();
    }

    public async Task<List<Product>?> ModifyProduct(int id, Product product)
    {
        var products = await _context.Products.FindAsync(id);

        if (products is null) return null;

        products.Name = product.Name;
        products.url = product.url ?? string.Empty;
        products.tag = product.tag;
        products.price = product.price;

        await _context.SaveChangesAsync();

        return await _context.Products.ToListAsync();

    }

    public async Task<List<Product>?> DeleteProduct(int id)
    {
        var product = await _context.Products.FindAsync(id);

        if (product is null) return null;

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();

        return await _context.Products.ToListAsync();
    }

    public async Task<List<Product>?> DeleteProductBasedOffName(string name)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.Name.StartsWith(name) || p.Name.EndsWith(name));
        if (product is null) return null;

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();

        return await _context.Products.ToListAsync();
    }
}