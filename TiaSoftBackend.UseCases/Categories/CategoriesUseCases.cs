namespace TiaSoftBackend.UseCases.Categories;

public record class CategoriesUseCases(
    GetCategories GetCategories, CreateCategory CreateCategory, UpdateCategory UpdateCategory);