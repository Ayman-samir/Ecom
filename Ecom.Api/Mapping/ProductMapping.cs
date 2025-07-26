using AutoMapper;
using Ecom.core.Dtos.Photo;
using Ecom.core.Dtos.Products;
using Ecom.core.Entities.Product;

namespace Ecom.Api.Mapping
{
    public class ProductMapping : Profile
    {
        public ProductMapping()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(productDto => productDto.CategoryName,
                op => op.MapFrom(src => src.Category.Name)).ReverseMap();
            CreateMap<Photo, PhotoDto>().ReverseMap();
            CreateMap<Product, ProductBasicDto>().ReverseMap();
            CreateMap<AddProductDto, Product>()
                .ForMember(dest => dest.Photos, op => op.Ignore()).ReverseMap();
            CreateMap<UpdateProductDto, Product>()
               .ForMember(dest => dest.Photos, op => op.Ignore()).ReverseMap();
            //CreateMap<TSource,TDestination>
        }
    }
}
