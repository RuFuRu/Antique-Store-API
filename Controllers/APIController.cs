using System.Net;
using Antique_Store_API.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Antique_Store_API.Controllers;
[Route("api/v1/products")]
[ApiController]
public class APIController : ControllerBase
{
    private readonly IDbOperation _products;

    public APIController(IDbOperation products)
    {
        _products = products;
    }

    
    /// <summary>
    /// Gets all the Products from the database. The response can be filtered to by name or tag
    /// </summary>
    /// <returns>Returns a list of all products available in the database.</returns>
    /// <response code="200">Returns all products</response>
    /// <response code="404">No products were found</response>
    /// <response code="500">There seem to have been an error on the server. Try again!</response>
    [EnableCors("corspolicy")]
    [HttpGet]
    public async Task<ActionResult<List<Product>>> getAllProducts([FromQuery] string? name, [FromQuery] string? tag)
    {
        if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(tag))
        {
            var result = await _products.GetProductByNameAndTag(name, tag);
            return Ok(result);
        }
        if (!string.IsNullOrEmpty(name))
        {
            var result = await _products.GetProductByName(name);
            //if (result is null) return NotFound("Product with that name has not been found");
            return Ok(result);
        } 
        if (!string.IsNullOrEmpty(tag))
        {
            var result = await _products.GetProductsByTag(tag);
            //if (result is null) return NotFound("Products with these tags have not been found");
            return Ok(result);
        }
        return await _products.GetAllProducts();
    }

    /// <summary>
    /// Gets a specific Product by its unique identifier.
    /// </summary>
    /// <param name="id">The ID of the Product to retrieve.</param>
    /// <returns>Returns the Product with the specified ID, if found.</returns>
    /// <response code="200">Returns a product that matches the provided id</response>
    /// <response code="404">No products with the provided id were found</response>
    /// <response code="500">There seem to have been an error on the server. Try again!</response>
    [EnableCors("corspolicy")]
    [HttpGet("{id}")]
    public async Task<ActionResult<Product?>> getProductById(int id)
    {
        var result = await _products.GetProductById(id);
        if (result is null) return NotFound("Product with this id has not been found");
        return Ok(result);

    }

    /// <summary>
    /// Adds a new Product to the database.
    /// </summary>
    /// <param name="product">The Product object to be added.</param>
    /// <returns>Returns the list of Products after adding the new Product.</returns>
    /// <response code="201">Adds the provided product into the database</response>
    /// <response code="500">There seem to have been an error on the server. Try again!</response>
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
    /// <response code="500">There seem to have been an error on the server. Try again!</response>
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
    /// <response code="500">There seem to have been an error on the server. Try again!</response>
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
    /// <response code="500">There seem to have been an error on the server. Try again!</response>
    [HttpDelete]
    public async Task<ActionResult<List<Product>?>> deleteProductBasedOffName([FromQuery] string name)
    {
        var result = await _products.DeleteProductBasedOffName(name);
        if(result is null) return NotFound("Products with this name has not been found");
        return Ok(result);
    }
}