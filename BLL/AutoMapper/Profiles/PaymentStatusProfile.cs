using AutoMapper;
using BLL.DTOs;
using DAL.Entities;

namespace BLL.AutoMapper.Profiles;

public class PaymentStatusProfile : Profile
{
    public PaymentStatusProfile()
    {
        CreateMap<PaymentStatus, PaymentStatusDTO>();
        CreateMap<PaymentStatusDTO, PaymentStatus>();
    }
} 