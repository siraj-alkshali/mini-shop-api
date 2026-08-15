using AutoMapper;
using MiniShop.Application.DTOs.Products;
using MiniShop.Domain.Entities;

namespace MiniShop.Application.Mapping;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductDto>();
        CreateMap<CreateProductRequest, Product>();
        CreateMap<UpdateProductRequest, Product>();
    }
}