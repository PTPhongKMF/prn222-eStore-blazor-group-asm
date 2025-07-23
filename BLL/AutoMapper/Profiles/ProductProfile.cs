using AutoMapper;
using BLL.DTOs;
using DAL.Entities;

namespace BLL.AutoMapper.Profiles;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductDTO>()
               .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.CategoryName));

        CreateMap<Product, ProductCreateDTO>();
        CreateMap<Product, ProductUpdateDTO>();

        // DTO to Entity mappings
        CreateMap<ProductDTO, Product>()
            .ForMember(dest => dest.Category, opt => opt.Ignore())
            .ForMember(dest => dest.OrderDetails, opt => opt.Ignore());

        CreateMap<ProductCreateDTO, Product>()
            .ForMember(dest => dest.ProductId, opt => opt.Ignore())
            .ForMember(dest => dest.Category, opt => opt.Ignore())
            .ForMember(dest => dest.OrderDetails, opt => opt.Ignore());

        CreateMap<ProductUpdateDTO, Product>()
            .ForMember(dest => dest.Category, opt => opt.Ignore())
            .ForMember(dest => dest.OrderDetails, opt => opt.Ignore());
    }
} 