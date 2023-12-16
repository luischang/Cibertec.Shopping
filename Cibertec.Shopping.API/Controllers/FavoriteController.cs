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
    public class FavoriteController : ControllerBase
    {
        private readonly IFavoriteService _favoriteService;

        public FavoriteController(IFavoriteService favoriteService)
        {
            _favoriteService = favoriteService;
        }

        [HttpPost]
        public async Task<IActionResult> Insert(FavoriteInsertDTO favoriteInsertDTO)
        {
            var result = await _favoriteService.Insert(favoriteInsertDTO);
            if (!result)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _favoriteService.Delete(id);
            if(!result)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetAll(int userId)
        {
            var favorites = await _favoriteService.GetAll(userId);
            if(favorites == null)
                return NotFound();

            return Ok(favorites);        
        }
    }
}
