using AutoMapper;
using BLL.DTOs;
using DAL.Entities;

namespace BLL.AutoMapper.Profiles;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductDTO>();
        CreateMap<ProductDTO, Product>()
            .ForMember(dest => dest.Category, opt => opt.Ignore())
            .ForMember(dest => dest.OrderDetails, opt => opt.Ignore());
    }
}