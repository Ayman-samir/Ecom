namespace Ecom.core.Dtos.Category
{
    public record CategoryDto(string Name, string Description);
    public record UpdateCategoryDto(string Name, string Description, int Id);

}
