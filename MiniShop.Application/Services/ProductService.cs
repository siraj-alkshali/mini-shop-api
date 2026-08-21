using AutoMapper;
using MiniShop.Application.DTOs.Common;
using MiniShop.Application.DTOs.Products;
using MiniShop.Application.Interfaces;
using MiniShop.Domain.Entities;

namespace MiniShop.Application.Services;

public class ProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ProductDto?> GetByIdAsync(int productId)
    {
        Product? product = await _productRepository.GetByIdAsync(productId);

        return product is null ? null : _mapper.Map<ProductDto>(product);
    }

    public async Task<ProductDto> CreateAsync(CreateProductRequest request)
    {
        Product product = _mapper.Map<Product>(request);

        await _productRepository.AddAsync(product);

        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<ProductDto>(product);
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

    public async Task<PagedResult<ProductDto>> GetProductsAsync(ProductQueryParameters parameters)
    {
        (List<Product> items, int totalItems) = await _productRepository.GetProductsAsync(parameters);

        List<ProductDto> products = _mapper.Map<List<ProductDto>>(items);

        return new PagedResult<ProductDto>
        {
            Items = products,
            TotalItems = totalItems,
            PageNumber = parameters.PageNumber,
            PageSize = parameters.PageSize
        };
    }
}