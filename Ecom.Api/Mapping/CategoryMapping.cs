using AutoMapper;
using Ecom.core.Dtos.Category;
using Ecom.core.Entities.Product;

namespace Ecom.Api.Mapping
{
    public class CategoryMapping : Profile
    {
        public CategoryMapping()
        {
            CreateMap<CategoryDto, Category>().ReverseMap();
            CreateMap<UpdateCategoryDto, Category>().ReverseMap();
        }
    }
}
