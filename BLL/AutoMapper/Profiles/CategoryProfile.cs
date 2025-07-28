using AutoMapper;
using BLL.DTOs;
using DAL.Entities;

namespace BLL.AutoMapper.Profiles;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<Category, CategoryDTO>();
        CreateMap<CategoryDTO, Category>();
    }
}