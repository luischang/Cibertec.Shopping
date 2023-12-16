using Cibertec.Shopping.CORE.DTOs;
using Cibertec.Shopping.CORE.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cibertec.Shopping.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetAll();
            if(products == null)
                return NotFound();

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productService.GetById(id);
            if(product == null)
                return NotFound();

            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] ProductInsertDTO productInsertDTO)
        {
            var result = await _productService.Insert(productInsertDTO);
            if(!result)
                return BadRequest();
            
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProductUpdateDTO productUpdateDTO)
        {
            if(id != productUpdateDTO.Id)
                return BadRequest();

            var result = await _productService.Update(productUpdateDTO);
            if(!result)
                return BadRequest();

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        { 
            var result = await _productService.Delete(id);
            if(!result)
                return BadRequest();

            return Ok(result);
        }


    }
}
