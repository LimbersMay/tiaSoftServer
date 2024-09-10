using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TiaSoftBackend.Entities;
using TiaSoftBackend.Models.Category;
using TiaSoftBackend.Services;

namespace TiaSoftBackend.controllers;

[ApiController]
[Route("api/categories")]
public class CategoriesController: ControllerBase
{

    private readonly ICategoriesRepository _categories;
    
    public CategoriesController(ICategoriesRepository categories)
    {
        _categories = categories;
    }

    [HttpGet]
    [Authorize(Roles = "SuperUsuario, Gerente, Capitan")]
    public async Task<IActionResult> GetCategories()
    {
        var categories = await _categories.GetCategories();

        var categoriesDto = categories.Select(category => new CategoryResponseDto()
        {
            CategoryId = category.CategoryId,
            Description = category.Description,
            Name = category.Name
        });
        
        return new JsonResult(categoriesDto);
    }

    [HttpPost]
    [Authorize(Roles = "SuperUsuario, Gerente, Capitan")]
    public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDto createCategoryDto)
    {
        var category = new Category()
        {
            CategoryId = Guid.NewGuid().ToString(),
            Name = createCategoryDto.Name,
            Description = createCategoryDto.Description
        };

        var categoryResult = await _categories.CreateCategory(category);
        
        return new JsonResult(new CategoryResponseDto()
        {
            CategoryId = categoryResult.CategoryId,
            Description = categoryResult.Description,
            Name = categoryResult.Name
        });
    }

    [HttpPut("{categoryId}")]
    [Authorize(Roles = "SuperUsuario, Gerente, Capitan")]
    public async Task<IActionResult> UpdateCategory(string categoryId, [FromBody] UpdateCategoryDto updateCategoryDto)
    {
        var category = new Category()
        {
            CategoryId = categoryId,
            Name = updateCategoryDto.Name,
            Description = updateCategoryDto.Description
        };

        var categoryResult = await _categories.UpdateCategory(category);
        
        return new JsonResult(new CategoryResponseDto()
        {
            CategoryId = categoryResult.CategoryId,
            Description = categoryResult.Description,
            Name = categoryResult.Name
        });
    }
}