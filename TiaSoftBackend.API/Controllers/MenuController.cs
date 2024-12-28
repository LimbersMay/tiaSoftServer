using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ROP.APIExtensions;
using TiaSoftBackend.DTOs.Menu;
using TiaSoftBackend.UseCases.Menus;

namespace TiaSoftBackend.API.Controllers;

[ApiController]
[Route("api/products")]
public class MenuController (MenuUseCases menu): ControllerBase
{
    [HttpGet]
    [Authorize(Roles = "SuperUsuario, Gerente, Capitan")]
    public async Task<IActionResult> GetMenu() 
        => await menu.GetMenus.Execute()
            .ToValueOrProblemDetails();

    [HttpPost]
    [Authorize(Roles = "SuperUsuario, Gerente, Capitan")]
    public async Task<IActionResult> CreateProduct([FromBody] CreateMenuRequest request)
        => await menu.CreateMenu.Execute(request)
            .ToValueOrProblemDetails();
    
    [HttpPut("{productId}")]
    [Authorize(Roles = "SuperUsuario, Gerente, Capitan")]
    public async Task<IActionResult> UpdateProduct(string productId, [FromBody] UpdateMenuRequest request)
        => await menu.UpdateMenu.Execute(request, productId)
            .ToValueOrProblemDetails();
    
    [HttpPost("updateImage/{productId}")]
    [Authorize(Roles = "SuperUsuario, Gerente, Capitan")]
    public async Task<IActionResult> UpdateProductImage(string productId, IFormFile image)
    {
        var uploadedFile = new UploadedFile
        {
            FileName = image.FileName,
            ContentType = image.ContentType,
            Length = image.Length,
            Content = image.OpenReadStream()
        };
        
        // Get the path where the image will be saved
        var basePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

        return await menu.UploadMenuImage.Execute(productId, uploadedFile, basePath)
            .ToValueOrProblemDetails();
    }
}