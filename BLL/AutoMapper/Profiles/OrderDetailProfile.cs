using AutoMapper;
using BLL.DTOs;
using DAL.Entities;

namespace BLL.AutoMapper.Profiles;

public class OrderDetailProfile : Profile
{
    public OrderDetailProfile()
    {
        CreateMap<OrderDetail, OrderDetailDTO>();
        CreateMap<OrderDetailDTO, OrderDetail>();
    }
} 