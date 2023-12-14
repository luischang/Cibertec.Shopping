using Cibertec.Shopping.CORE.DTOs;
using Cibertec.Shopping.CORE.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Cibertec.Shopping.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> SignUp([FromBody] UserInsertDTO userInsertDTO)
        {
            if (userInsertDTO.Email.IsNullOrEmpty() || userInsertDTO.Password.IsNullOrEmpty())
                return BadRequest();

            var result = await _userService.SignUp(userInsertDTO);
            if (!result)
                return BadRequest();

            return NoContent();
        }

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn([FromBody] UserLoginDTO userLoginDTO)
        {
            if(userLoginDTO.Email.IsNullOrEmpty() || userLoginDTO.Password.IsNullOrEmpty())
                return BadRequest();

            var result = await _userService.SignIn(userLoginDTO);
            if(result == null)
                return NotFound();

            return Ok(result);
        }
    }
}
