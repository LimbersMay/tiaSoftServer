using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TiaSoftBackend.Entities;
using TiaSoftBackend.Models.Menu;
using TiaSoftBackend.Models.Product;
using TiaSoftBackend.Services;

namespace TiaSoftBackend.controllers;

[ApiController]
[Route("api/products")]
public class MenuController: ControllerBase
{
    private IMenuRepository _menuRepository;
    private IMapper _mapper;
    
    public MenuController(IMenuRepository menuRepository, IMapper mapper)
    {
        _menuRepository = menuRepository;
        _mapper = mapper;
    }

    [HttpGet]
    [Authorize(Roles = "SuperUsuario, Gerente, Capitan")]
    public async Task<IActionResult> GetMenu()
    {
        var menu = await _menuRepository.GetMenu();
        var menusMapped = _mapper.Map<IEnumerable<ProductResponseDto>>(menu);
        
        return new JsonResult(menusMapped);
    }

    [HttpPost]
    [Authorize(Roles = "SuperUsuario, Gerente, Capitan")]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto product)
    {
        var productMapped = _mapper.Map<Product>(product);
        productMapped.ProductId = Guid.NewGuid().ToString();
        
        var newProduct = await _menuRepository.CreateProduct(productMapped);
        return new JsonResult(newProduct);
    }
    
    [HttpPut("{productId}")]
    [Authorize(Roles = "SuperUsuario, Gerente, Capitan")]
    public async Task<IActionResult> UpdateProduct(string productId, [FromBody] UpdateProductDto product)
    {
        var productMapped = _mapper.Map<Product>(product);
        
        productMapped.ProductId = productId;
        
        var updatedProduct = await _menuRepository.UpdateProduct(productMapped);
        var updateProductResponse = _mapper.Map<ProductResponseDto>(updatedProduct);
        
        return new JsonResult(updateProductResponse);
    }
    
    [HttpPost("updateImage/{productId}")]
    [Authorize(Roles = "SuperUsuario, Gerente, Capitan")]
    public async Task<IActionResult> UpdateProductImage(string productId, [FromForm] IFormFile image)
    {
        // Check if the product exists
        var product = await _menuRepository.GetProductById(productId);
        
        if (product == null)
        {
            return NotFound("Producto no encontrado.");
        }
        
        // Define the path where the file will be saved
        var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
        
        // Remove the old image if it exists
        if (!string.IsNullOrEmpty(product.ImageUrl))
        {
            Console.WriteLine(product.ImageUrl);
            var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", product.ImageUrl);
            if (System.IO.File.Exists(oldImagePath))
            {
                Console.WriteLine("Deleting old image..." + oldImagePath);
                System.IO.File.Delete(oldImagePath);
            }
            else
            {
                Console.WriteLine("Old image not found.");
            }
        }
        
        // Validate if the file is valid
        if (image == null || image.Length == 0 || image.ContentType is null)
        {
            Console.WriteLine(image);
            return BadRequest("La imagen no puede ser subida.");
        }
        // Generate a unique file name
        var fileExtension = Path.GetExtension(image.FileName); // Obtener extensión del archivo
        var fileName = $"{Guid.NewGuid()}{fileExtension}"; // Generar GUID + extensión

        // Create the directory if it doesn't exist
        if (!Directory.Exists(uploadPath))
        {
            Directory.CreateDirectory(uploadPath);
        }

        // Combine the path with the file name
        var filePath = Path.Combine(uploadPath, fileName);

        // Save the file to the server
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await image.CopyToAsync(stream);
        }

        // Here we define the relative path to the file
        // Ex.: "/images/{fileName}"
        var relativeFilePath = $"images/{fileName}";
        
        // Check if the file exists
        Console.WriteLine(filePath);
        Console.WriteLine(System.IO.File.Exists(filePath));
        
        var result = await _menuRepository.UpdateProductImage(productId, relativeFilePath);

        if (!result)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "No se pudo actualizar la imagen del producto.");
        }

        return Ok(new
        {
            imagePath = relativeFilePath
        });
    }
}