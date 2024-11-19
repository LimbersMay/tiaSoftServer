using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ROP.APIExtensions;
using TiaSoftBackend.DTOs.Categories;
using TiaSoftBackend.UseCases.Categories;

namespace TiaSoftBackend.API.Controllers;

[ApiController]
[Route("api/categories")]
public class CategoriesController (CategoriesUseCases categories) : ControllerBase
{

    [HttpGet]
    [Authorize(Roles = "SuperUsuario, Gerente, Capitan")]
    public async Task<IActionResult> GetCategories()
        => await categories.GetCategories.Execute()
            .ToValueOrProblemDetails();

    [HttpPost]
    [Authorize(Roles = "SuperUsuario, Gerente, Capitan")]
    public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryRequest request)
        => await categories.CreateCategory.Execute(request)
            .ToValueOrProblemDetails();

    [HttpPut("{categoryId}")]
    [Authorize(Roles = "SuperUsuario, Gerente, Capitan")]
    public async Task<IActionResult> UpdateCategory(string categoryId, [FromBody] UpdateCategoryRequest request)
        => await categories.UpdateCategory.Execute(categoryId, request)
            .ToValueOrProblemDetails();
}