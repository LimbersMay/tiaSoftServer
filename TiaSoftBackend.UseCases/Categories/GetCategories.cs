using AutoMapper;
using ROP;
using TiaSoftBackend.Data.Repositories;
using TiaSoftBackend.DTOs.Categories;

namespace TiaSoftBackend.UseCases.Categories;

public class GetCategories (ICategoriesRepository categoriesRepository, IMapper mapper)
{
    public async Task<Result<List<CategoryDto>>> Execute()
    {
        var categories = await categoriesRepository.GetCategories();
        return mapper.Map<List<CategoryDto>>(categories);
    }
}