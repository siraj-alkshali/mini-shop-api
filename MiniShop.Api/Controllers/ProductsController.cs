using Microsoft.AspNetCore.Mvc;
using MiniShop.Application.DTOs.Products;
using MiniShop.Application.Services;

namespace MiniShop.Api.Controllers;

[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    private readonly ProductService _productService;

    public ProductsController(ProductService productService)
    {
        _productService = productService;
    }

    [HttpGet("{productId:int}")]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductDto>> GetById(int productId)
    {
        ProductDto? product = await _productService.GetByIdAsync(productId);

        return product is null ? NotFound() : Ok(product);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status201Created)]
    public async Task<ActionResult<ProductDto>> Create(CreateProductRequest request)
    {
        ProductDto product = await _productService.CreateAsync(request);

        return CreatedAtAction(nameof(GetById), new { productId = product.Id }, product);
    }

    [HttpPut("{productId:int}")]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductDto>> Update(int productId, UpdateProductRequest request)
    {
        ProductDto? updatedProduct = await _productService.UpdateAsync(productId, request);

        return updatedProduct is null ? NotFound() : Ok(updatedProduct);
    }

    [HttpDelete("{productId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int productId)
    {
        bool deleted = await _productService.DeleteAsync(productId);

        return !deleted ? NotFound() : NoContent();
    }
}