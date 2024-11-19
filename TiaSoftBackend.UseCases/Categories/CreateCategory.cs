using AutoMapper;
using ROP;
using TiaSoftBackend.Data.Entities;
using TiaSoftBackend.Data.Repositories;
using TiaSoftBackend.DTOs.Categories;

namespace TiaSoftBackend.UseCases.Categories;

public class CreateCategory (ICategoriesRepository categoriesRepository, IMapper mapper)
{
    public async Task<Result<CategoryDto>> Execute(CreateCategoryRequest request)
    {
        var category = mapper.Map<Category>(request);
        category.CategoryId = Guid.NewGuid().ToString();
        
        var categoryResult = await categoriesRepository.CreateCategory(category);
        return mapper.Map<CategoryDto>(categoryResult);
    }
}