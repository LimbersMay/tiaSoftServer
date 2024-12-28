using AutoMapper;
using TiaSoftBackend.Data.Entities;
using TiaSoftBackend.DTOs.Categories;

namespace TiaSoftBackend.UseCases.Mappers;

public class CategoriesProfile : Profile
{
    public CategoriesProfile()
    {
        CreateMap<CreateCategoryRequest, Category>();
        CreateMap<Category, CategoryDto>();
    }
}