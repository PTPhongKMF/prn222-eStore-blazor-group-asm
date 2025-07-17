using AutoMapper;
using BLL.DTOs;
using DAL.Entities;

namespace BLL.AutoMapper.Profiles;

public class OrderStatusProfile : Profile
{
    public OrderStatusProfile()
    {
        CreateMap<OrderStatus, OrderStatusDTO>();
        CreateMap<OrderStatusDTO, OrderStatus>();
    }
} 