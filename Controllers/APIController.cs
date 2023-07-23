using System.Net;
using Antique_Store_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Antique_Store_API.Controllers;
[Route("api/v1/product/")]
[ApiController]
public class APIController : ControllerBase
{
    private readonly IDbOperation _products;

    public APIController(IDbOperation products)
    {
        _products = products;
    }

    
    /// <summary>
    /// Gets all the Products from the database.
    /// </summary>
    /// <returns>Returns a list of all products available in the database.</returns>
    /// <response code="200">Returns all products</response>
    /// <response code="404">No products were found</response>
    [HttpGet]
    public async Task<ActionResult<List<Product>>> getAllProducts()
    {
        return await _products.GetAllProducts();
    }

    /// <summary>
    /// Gets a specific Product by its unique identifier.
    /// </summary>
    /// <param name="id">The ID of the Product to retrieve.</param>
    /// <returns>Returns the Product with the specified ID, if found.</returns>
    /// <response code="200">Returns a product that matches the provided id</response>
    /// <response code="404">No products with the provided id were found</response>
    [HttpGet("{id}")]
    public async Task<ActionResult<Product?>> getProductById(int id)
    {
        var result = await _products.GetProductById(id);
        if (result is null) return NotFound("Product with this id has not been found");
        return Ok(result);

    }

    /// <summary>
    /// Gets Products based on the specified tag.
    /// </summary>
    /// <param name="tag">The tag to filter Products.</param>
    /// <returns>Returns a list of Products that match the specified tag.</returns>
    /// <response code="200">Returns all products matched by the provided tag name</response>
    /// <response code="404">No products with this tag name were found</response>
    [HttpGet("getbytag")]
    public async Task<ActionResult<List<Product>?>> getProductByTag([FromQuery] string tag)
    {
        var result = await _products.GetProductsByTag(tag);
        if (result is null) return NotFound("Products with these tags have not been found");
        return Ok(result);
    }

    /// <summary>
    /// Gets a Product based on the specified name.
    /// </summary>
    /// <param name="name">The name to search for a Product.</param>
    /// <returns>Returns the Product that matches the specified name, if found.</returns>
    /// <response code="200">Returns all products matched by the provided product name</response>
    /// <response code="404">No Products with such name were found</response>
    [HttpGet("getbyname")]
    public async Task<ActionResult<Product>?> getProductByName([FromQuery] string name)
    {
        var result = await _products.GetProductByName(name);
        if (result is null) return NotFound("Product with that name has not been found");
        return Ok(result);
    }

    /// <summary>
    /// Adds a new Product to the database.
    /// </summary>
    /// <param name="product">The Product object to be added.</param>
    /// <returns>Returns the list of Products after adding the new Product.</returns>
    /// <response code="201">Adds the provided product into the database</response>
    [HttpPost]
    public async Task<ActionResult<List<Product>>> addProduct(Product product)
    {
        var result = await _products.AddProduct(product);
        return StatusCode(StatusCodes.Status201Created, result);
    }

    /// <summary>
    /// Modifies an existing Product with the specified ID.
    /// </summary>
    /// <param name="id">The ID of the Product to be modified.</param>
    /// <param name="product">The updated Product object.</param>
    /// <returns>Returns the list of Products after modifying the existing Product.</returns>
    /// <response code="200">Modifies the product with the provided id </response>
    /// <response code="404">No Products with such id were found</response>
    [HttpPut("{id}")]
    public async Task<ActionResult<List<Product>?>> modifyProduct(int id, Product product)
    {
        var result = await _products.ModifyProduct(id, product);
        if (result is null) return NotFound("Products with this id has not been found");
        return Ok(result);
    }

    /// <summary>
    /// Deletes a Product from the database based on the specified ID.
    /// </summary>
    /// <param name="id">The ID of the Product to be deleted.</param>
    /// <returns>Returns the list of Products after deleting the specified Product.</returns>
    /// <response code="200">Deletes the product with the provided id </response>
    /// <response code="404">No Products with such id were found</response>
    [HttpDelete("{id}")]
    public async Task<ActionResult<List<Product>?>> deleteProduct(int id)
    {
        var result = await _products.DeleteProduct(id);
        if(result is null) return NotFound("Products with this id has not been found");
        return Ok(result);
    }

    /// <summary>
    /// Deletes Products from the database based on the specified name.
    /// </summary>
    /// <param name="name">The name of the Products to be deleted.</param>
    /// <returns>Returns the list of Products after deleting the specified Products by name.</returns>
    /// <response code="200">Deletes the product with the provided name </response>
    /// <response code="404">No Products with such name were found</response>
    [HttpDelete("deletebyname")]
    public async Task<ActionResult<List<Product>?>> deleteProductBasedOffName([FromQuery] string name)
    {
        var result = await _products.DeleteProductBasedOffName(name);
        if(result is null) return NotFound("Products with this name has not been found");
        return Ok(result);
    }
}