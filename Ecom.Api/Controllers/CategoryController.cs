using AutoMapper;
using Ecom.Api.helper;
using Ecom.core.Dtos.Category;
using Ecom.core.Entities.Product;
using Ecom.core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : BaseController
    {

        public CategoryController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var categories = await _unitOfWork.CategoryRepository.GetAllAsync();
                if (categories is null)
                {
                    return NotFound(new ResponseApi(404, "Items Not Found"));
                }
                return Ok(categories);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("getById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var category = await _unitOfWork.CategoryRepository.GetByIdAsync(id);
                //if (category is null) return Ok(new ResponseApi(400, "Item Not Found"));
                // if (category is null) return BadRequest(new ResponseApi(400, "Item Not Found"));
                if (category is null) return NotFound(new ResponseApi(404, "Item Not Found"));

                return Ok(category);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("AddCategory")]
        public async Task<IActionResult> AddCategory(CategoryDto categoryDto)
        {
            try
            {
                /*var category = new Category()
                {
                    Name = categoryDto.Name,
                    Description = categoryDto.Description,
                };*/
                //source =>مصدر اللي هحول منه
                //destination =>الوجه اللي هحول اليها
                var category = _mapper.Map<Category>(categoryDto);
                await _unitOfWork.CategoryRepository.AddAsync(category);
                await _unitOfWork.CompleteAsync();
                return Ok(new ResponseApi(201, "New Item has been added "));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("UpdateCategory/{id}")]
        public async Task<IActionResult> UpdateCategory(int id, UpdateCategoryDto categoryDto)
        {
            try
            {
                /*var category = new Category()
                {
                    Name = categoryDto.Name,
                    Description = categoryDto?.Description,
                    Id = categoryDto.Id
                };*/
                /* var category = await _unitOfWork.CategoryRepository.GetByIdAsync(categoryDto.Id);
                 if (category is null)
                 {
                     return NotFound(new { message = "Category not found" });
                 }
                 category.Name = categoryDto.Name ?? category.Name;
                 category.Description = categoryDto.Description ?? category.Description;*/
                var category = _mapper.Map<Category>(UpdateCategory);
                await _unitOfWork.CategoryRepository.UpdateAsync(id, category);
                await _unitOfWork.CompleteAsync();
                return Ok(new ResponseApi(200, "Item has been updated "));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DelateCategory/{id}")]
        public async Task<IActionResult> DelateCategory(int id)
        {
            try
            {
                await _unitOfWork.CategoryRepository.DeleteAsync(id);
                await _unitOfWork.CompleteAsync();
                return Ok(new ResponseApi(200, "Item has been Deleted "));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }


    }
}
