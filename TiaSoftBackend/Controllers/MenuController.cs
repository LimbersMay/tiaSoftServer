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
}