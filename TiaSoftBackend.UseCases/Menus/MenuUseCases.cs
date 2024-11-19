namespace TiaSoftBackend.UseCases.Menus;

public record class MenuUseCases (
    GetMenus GetMenus, 
    CreateMenu CreateMenu, 
    UpdateMenu UpdateMenu,
    UploadMenuImage UploadMenuImage);