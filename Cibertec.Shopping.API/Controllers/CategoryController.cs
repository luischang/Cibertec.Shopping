using Cibertec.Shopping.CORE.DTOs;
using Cibertec.Shopping.CORE.Entities;
using Cibertec.Shopping.CORE.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cibertec.Shopping.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(ICategoryService categoryService, ILogger<CategoryController> logger)
        {
            _categoryService = categoryService;
            _logger = logger;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            throw new Exception("Error to get all categories...... Admin suport SOS");

            _logger.LogInformation("Initial Get all categories.....");
            var categories = await _categoryService.GetAll();
            
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, bool includeProducts = false)
        {
            var category = await _categoryService.GetById(id, includeProducts);
            if (category == null)
                return NotFound();

            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CategoryInsertDTO categoryInsertDTO)
        {
            var result = await _categoryService.Insert(categoryInsertDTO);
            if (!result)
                return BadRequest();

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CategoryListDTO categoryListDTO)
        {
            if (id != categoryListDTO.Id)
                return NotFound();

            var result = await _categoryService.Update(categoryListDTO);
            if (!result)
                return BadRequest();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _categoryService.Delete(id);
            if (!result)
                return BadRequest();

            return NoContent();
        }

    }
}
