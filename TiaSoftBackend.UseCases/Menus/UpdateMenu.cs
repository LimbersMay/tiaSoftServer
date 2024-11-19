using AutoMapper;
using ROP;
using TiaSoftBackend.Data.Entities;
using TiaSoftBackend.Data.Repositories;
using TiaSoftBackend.DTOs.Menu;

namespace TiaSoftBackend.UseCases.Menus;

public class UpdateMenu (IMenuRepository menuRepository, IMapper mapper)
{
    public async Task<Result<MenuDto>> Execute(UpdateMenuRequest request, string menuId)
    {
        var menuEntity = mapper.Map<Product>(request);
        
        var updatedMenu = await menuRepository.UpdateProduct(menuEntity);
        
        return mapper.Map<MenuDto>(updatedMenu);
    }
}