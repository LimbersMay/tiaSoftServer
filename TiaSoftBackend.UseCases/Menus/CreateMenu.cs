using AutoMapper;
using ROP;
using TiaSoftBackend.Data.Entities;
using TiaSoftBackend.Data.Repositories;
using TiaSoftBackend.DTOs.Menu;

namespace TiaSoftBackend.UseCases.Menus;

public class CreateMenu (IMenuRepository menuRepository, IMapper mapper)
{
    public async Task<Result<MenuDto>> Execute(CreateMenuRequest request)
    {
        var menuEntity = mapper.Map<Product>(request);
        menuEntity.ProductId = Guid.NewGuid().ToString();
        
        var newMenu = await menuRepository.CreateProduct(menuEntity);
        
        return mapper.Map<MenuDto>(newMenu);
    }
}