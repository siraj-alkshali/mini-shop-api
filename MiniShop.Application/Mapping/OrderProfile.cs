using AutoMapper;
using MiniShop.Application.DTOs.Customers;
using MiniShop.Domain.Entities;

namespace MiniShop.Application.Mapping;

public class CustomerProfile : Profile
{
    public CustomerProfile()
    {
        CreateMap<Customer, CustomerDto>()
        .ForMember(
            dest => dest.Username,
            opt => opt.MapFrom(src => src.User.Username)
        );
    }
}