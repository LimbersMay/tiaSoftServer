using AutoMapper;
using ROP;
using TiaSoftBackend.Data.Repositories;
using TiaSoftBackend.DTOs.Menu;

namespace TiaSoftBackend.UseCases.Menus;

public class GetMenus (IMenuRepository menuRepository, IMapper mapper)
{
    public async Task<Result<List<MenuDto>>> Execute()
    {
        var menus = await menuRepository.GetMenu();
        return mapper.Map<List<MenuDto>>(menus);
    }
}