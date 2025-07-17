using AutoMapper;
using BLL.DTOs;
using DAL.Entities;

namespace BLL.AutoMapper.Profiles;

public class CartProfile : Profile
{
    public CartProfile()
    {
        CreateMap<Cart, CartDTO>();
        CreateMap<CartDTO, Cart>();
    }
} 