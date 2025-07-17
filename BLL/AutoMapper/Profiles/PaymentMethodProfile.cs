using AutoMapper;
using BLL.DTOs;
using DAL.Entities;

namespace BLL.AutoMapper.Profiles;

public class PaymentMethodProfile : Profile
{
    public PaymentMethodProfile()
    {
        CreateMap<PaymentMethod, PaymentMethodDTO>();
        CreateMap<PaymentMethodDTO, PaymentMethod>();
    }
} 