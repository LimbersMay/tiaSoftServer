using AutoMapper;
using ROP;
using TiaSoftBackend.Data.Entities;
using TiaSoftBackend.Data.Repositories;
using TiaSoftBackend.DTOs.Categories;

namespace TiaSoftBackend.UseCases.Categories;

public class UpdateCategory (ICategoriesRepository categoriesRepository, IMapper mapper)
{
    public async Task<Result<CategoryDto>> Execute(string categoryId, UpdateCategoryRequest request)
    {
        var categoryEntity = mapper.Map<Category>(request);
        
        categoryEntity.CategoryId = categoryId;
        
        var updatedCategory = await categoriesRepository.UpdateCategory(categoryEntity);
        
        return mapper.Map<CategoryDto>(updatedCategory);
    }
}