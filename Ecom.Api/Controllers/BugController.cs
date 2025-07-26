using AutoMapper;
using Ecom.core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BugController : BaseController
    {
        public BugController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        [HttpGet("NotFound")]
        public async Task<IActionResult> GetNotFound()
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(100);
            if (category == null) return NotFound();
            return Ok(category);
        }
        [HttpGet("ServerError")]
        public async Task<IActionResult> GetServerError()
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(100);
            category.Name = "Error";
            return Ok(category);
            /*throw new Exception("Test Global Exception Handling");*/
        }
        [HttpGet("BadRequest/{id}")]
        public async Task<IActionResult> GetBadRequest(int id)
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(100);

            return Ok(category);
        }
        [HttpGet("BadRequest")]
        public async Task<IActionResult> GetBadRequest()
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(100);

            return BadRequest();
        }
    }
}
