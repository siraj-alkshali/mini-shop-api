using Microsoft.AspNetCore.Mvc;
using MiniShop.Application.DTOs.Products;
using MiniShop.Application.Services;
using MiniShop.Domain.Entities;

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

        if (product is null)
            return NotFound();

        return Ok(product);
    }
}