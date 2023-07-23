namespace Antique_Store_API.Models;

public interface IDbOperation
{
    public Task<List<Product>> GetAllProducts();
    public Task<Product?> GetProductById(int id);
    public Task<List<Product>?> GetProductsByTag(string tag);
    public Task<List<Product>?> GetProductByName(string name);
    public Task<List<Product>> AddProduct(Product product);
    public Task<List<Product>?> ModifyProduct(int id, Product product);
    public Task<List<Product>?> DeleteProduct(int id);
    public Task<List<Product>?> DeleteProductBasedOffName(string name);
}