using ROP;
using TiaSoftBackend.Data.Repositories;

namespace TiaSoftBackend.UseCases.Menus;

public partial class UploadedFile
{
    public required string FileName { get; set; }
    public required string ContentType { get; set; }
    public required long Length { get; set; }
    public required Stream Content { get; set; }
}

public class UploadMenuImage(IMenuRepository menuRepository)
{
    public async Task<Result<string>> Execute(string productId, UploadedFile image, string basePath)
    {
        // Verify if the product exists
        var product = await menuRepository.GetProductById(productId);
        if (product is null)
        {
            return Result.NotFound<string>(ErrorCodes.ErrorCodes.MenuNotFound);
        }

        // Delete the old image if it exists
        if (!string.IsNullOrEmpty(product.ImageUrl))
        {
            var oldImagePath = Path.Combine(basePath, product.ImageUrl);
            if (File.Exists(oldImagePath))
            {
                Console.WriteLine("Deleting old image..." + oldImagePath);
                File.Delete(oldImagePath);
            }
            else
            {
                Console.WriteLine("Old image not found.");
            }
        }

        // Verify if the image is valid
        if (image == null || image.Content == null || string.IsNullOrEmpty(image.ContentType))
        {
            return Result.BadRequest<string>(ErrorCodes.ErrorCodes.MenuImageInvalid);
        }

        // Generate a unique file name
        var fileExtension = Path.GetExtension(image.FileName); // Obtiene la extensión del archivo
        var fileName = $"{Guid.NewGuid()}{fileExtension}"; // Genera GUID + extensión

        // Define the path where the image will be saved
        var uploadPath = Path.Combine(basePath, "images");

        // Create the directory if it doesn't exist
        if (!Directory.Exists(uploadPath))
        {
            Directory.CreateDirectory(uploadPath);
        }

        // Combine the path with the file name
        var filePath = Path.Combine(uploadPath, fileName);

        // Save the image
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await image.Content.CopyToAsync(stream); // We use CopyToAsync to avoid blocking the thread
        }

        // Define the relative path to the image
        var relativeFilePath = Path.Combine("images", fileName).Replace("\\", "/");

        // Update the product image
        var result = await menuRepository.UpdateProductImage(productId, relativeFilePath);
        if (!result)
        {
            return Result.Failure<string>(ErrorCodes.ErrorCodes.MenuImageCannotBeUploaded);
        }

        return Result.Success(relativeFilePath);
    }
}