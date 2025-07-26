using AutoMapper;
using Ecom.core.Dtos.Products;
using Ecom.core.Entities.Product;
using Ecom.core.Services;
using Ecom.Infrastructture.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecom.Infrastructture.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IImageMandmentService _imageMangmentService;
        public ProductRepository(AppDbContext context, IMapper mapper, IImageMandmentService imageMangmentService) : base(context)
        {
            _mapper = mapper;
            _context = context;
            _imageMangmentService = imageMangmentService;
        }
        public async Task<IEnumerable<ProductDto>> GetAllAsync(string sort, int? CategoryId, int pageSize, int pageNumber)
        {
            var query = _context.Products
                .Include(p => p.Category)
                .Include(p => p.Photos)
                .AsNoTracking();

            //filtering by catgory id
            if (CategoryId.HasValue)
            {
                query = query.Where(pr => pr.CategoryId == CategoryId);
            }

            if (!string.IsNullOrEmpty(sort))
            {
                //switch (sort)
                //{
                //    case "PriceAsc":
                //        query = query.OrderBy(p => p.NewPrice);
                //        break;
                //    case "PriceDecs":
                //        query = query.OrderByDescending(p => p.NewPrice);
                //        break;
                //    default:
                //        query = query.OrderBy(p => p.Name);
                //        break;
                //}
                query = sort switch
                {
                    "PriceAsc" => query.OrderBy(p => p.NewPrice),
                    "PriceDecs" => query.OrderByDescending(p => p.NewPrice),
                    _ => query.OrderBy(p => p.Name),
                };
            }
            pageNumber = pageNumber > 0 ? pageNumber : 0;
            pageSize = pageSize > 0 ? pageSize : 10;

            query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
            var result = _mapper.Map<IEnumerable<ProductDto>>(query);
            return result;
        }
        public async Task<bool> AddAsync(AddProductDto productDto)
        {
            if (productDto == null) return false;
            // _mapper.Map<Destenation>(source)
            var product = _mapper.Map<Product>(productDto);
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            var imagePath = await _imageMangmentService.AddImageAsync(productDto.Photo, productDto.Name);
            var photo = imagePath.Select(path => new Photo
            {
                ImageName = path,
                ProductId = product.Id,
            }).ToList();
            await _context.AddRangeAsync(photo);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateAsync(int id, UpdateProductDto updateProductDto)
        {
            if (updateProductDto is null) return false;
            var product = await _context.Products.Include(p => p.Category)
                .Include(p => p.Photos)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null) return false;
            //_mapper.Map<product>(UpdateProductDto);
            _mapper.Map(updateProductDto, product);
            var findPhotos = await _context.Photos.Where(ph => ph.ProductId == id).ToListAsync();
            foreach (var photo in findPhotos)
            {
                _imageMangmentService.DeleteImageAsync(photo.ImageName);
            }
            _context.Photos.RemoveRange(findPhotos);

            var imagePath = await _imageMangmentService.AddImageAsync(updateProductDto.Photo, updateProductDto.Name);
            var newPhoto = imagePath.Select(path => new Photo
            {
                ImageName = path,
                ProductId = id,
            }).ToList();
            await _context.AddRangeAsync(newPhoto);
            await _context.SaveChangesAsync();
            return true;
        }

        public new async Task<bool> DeleteAsync(int id) //new means take the delete method from IProductRespository
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                var photos = _context.Photos.Where(photo => photo.ProductId == product.Id);
                foreach (var photo in photos)
                {
                    _imageMangmentService.DeleteImageAsync(photo.ImageName);
                }
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;

        }

    }


}
