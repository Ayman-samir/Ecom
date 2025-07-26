using AutoMapper;
using Ecom.Api.helper;
using Ecom.core.Dtos.Products;
using Ecom.core.Interfaces;
using Ecom.core.Sharing;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : BaseController
    {
        public ProductsController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllProducts([FromQuery]ProductParams productParams)
        {
            try
            {
                //var products = await _unitOfWork.ProductRepository.GetAllAsync(x => x.Category, x => x.Photos);
                var products = await _unitOfWork.ProductRepository.GetAllAsync(productParams);
                //var products = await _unitOfWork.ProductRepository.GetAllAsync();
                if (products is null) return NotFound(new ResponseApi(404, "Products Not Founds"));
                /* var productDto = products.Select(p => new ProductDto
                 {
                     Name = p.Name,
                     Price = p.Price,
                     CategoryName=p.Category.Name,
                     Photos = p.Photos
                 });*/
                var productsDto = _mapper.Map<List<ProductDto>>(products);
                return Ok(productsDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            try
            {
                var product = await _unitOfWork.ProductRepository.GetByIdAsync(id);
                if (product is null) return NotFound(new ResponseApi(404, "Product Not Found"));
                var propDto = _mapper.Map<ProductBasicDto>(product);
                return Ok(propDto);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [HttpGet("GetByIdProductCategory/{id}")]
        public async Task<IActionResult> GetProductCategoryById(int id)
        {
            try
            {
                var product = await _unitOfWork.ProductRepository.GetByIdAsync(id, x => x.Category, x => x.Photos);
                if (product is null) return NotFound(new ResponseApi(404, "Product Not Found"));
                var propDto = _mapper.Map<ProductDto>(product);
                return Ok(propDto);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }
        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProduct(AddProductDto productDto)
        {
            try
            {
                await _unitOfWork.ProductRepository.AddAsync(productDto);
                return Ok(new ResponseApi(200, "Product has created Successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseApi(400, ex.Message));
            }
        }

        [HttpPatch("UpdateProduct/{id}")]
        public async Task<IActionResult> UpdateProduct(int id, UpdateProductDto updateProductDto)
        {
            try
            {
                await _unitOfWork.ProductRepository.UpdateAsync(id, updateProductDto);
                return Ok(new ResponseApi(200));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseApi(400, ex.Message));
            }
        }

        [HttpDelete("DeleteProduct/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                var result = await _unitOfWork.ProductRepository.DeleteAsync(id);
                if (result)
                {
                    return Ok(new ResponseApi(200, "Delete Product Sucessfully"));
                }
                return NotFound(new ResponseApi(404, "Product Not found"));

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);

            }

        }

    }
}
