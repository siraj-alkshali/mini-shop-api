using AutoMapper;
using MiniShop.Application.DTOs.Products;
using MiniShop.Application.Interfaces;
using MiniShop.Domain.Entities;

namespace MiniShop.Application.Services;

public class ProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<ProductDto?> GetByIdAsync(int productId)
    {
        Product? product = await _productRepository.GetByIdAsync(productId);

        if (product is null)
            return null;

        return _mapper.Map<ProductDto>(product);
    }

    public async Task<ProductDto> CreateAsync(CreateProductRequest request)
    {
        Product product = _mapper.Map<Product>(request);

        Product createdProduct = await _productRepository.AddAsync(product);

        return _mapper.Map<ProductDto>(createdProduct);
    }

    public async Task<ProductDto?> UpdateAsync(int productId, UpdateProductRequest request)
    {
        Product? product = await _productRepository.GetByIdAsync(productId);

        if (product is null)
            return null;

        _mapper.Map(request, product);

        await _productRepository.UpdateAsync(product);

        return _mapper.Map<ProductDto>(product);
    }

    public async Task<bool> DeleteAsync(int productId)
    {
        return await _productRepository.DeleteAsync(productId);
    }
}