using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using TheKnife.Entities.Efos;
using TheKnife.Services.Services;

namespace TheKnife.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterUsersController : ControllerBase
    {
        private readonly RegisterUsersService _registerUsersService;

        public RegisterUsersController(RegisterUsersService registerUsersService)
        {
            _registerUsersService=registerUsersService;
        }

        // GET api/registerusers
        [HttpGet]
        [ProducesResponseType(typeof(List<RegisterUsersEfo>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<RegisterUsersEfo>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(List<RegisterUsersEfo>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(List<RegisterUsersEfo>), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<List<RegisterUsersEfo>>> GetAllRegisterUsersAsync()
        {
            List<RegisterUsersEfo> registerUsers = await _registerUsersService.GetAllRegisterUsersAsync();

            if (registerUsers == null)
            {
                return StatusCode(StatusCodes.Status204NoContent);
            }

            return StatusCode(StatusCodes.Status200OK, registerUsers);
        }

        // GET api/registerusers/{id}
        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(RegisterUsersEfo), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(RegisterUsersEfo), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(RegisterUsersEfo), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(RegisterUsersEfo), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetRegisterUserByIdAsync(int id)
        {
            RegisterUsersEfo registerUser = await _registerUsersService.GetRegisterUserByIdAsync(id);

            if (registerUser == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            return StatusCode(StatusCodes.Status200OK, registerUser);
        }

        // POST api/registerusers
        [HttpPost]
        [ProducesResponseType(typeof(RegisterUsersEfo), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(RegisterUsersEfo), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(RegisterUsersEfo), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(RegisterUsersEfo), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<RegisterUsersEfo>> SendRegisterUserAsync([FromBody, Required] RegisterUsersEfo registerUser)
        {
            if (ModelState.IsValid)
            {
                RegisterUsersEfo newRegisterUser = await _registerUsersService
                    .SendRegisterUserAsync(registerUser);

                return StatusCode(StatusCodes.Status201Created, newRegisterUser);
            }

            return StatusCode(StatusCodes.Status400BadRequest, ModelState);
        }

        // POST api/registerusers/sendlogin
        [HttpPost("sendlogin")]
        [ProducesResponseType(typeof(RegisterUsersEfo), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(RegisterUsersEfo), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(RegisterUsersEfo), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(RegisterUsersEfo), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(RegisterUsersEfo), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<RegisterUsersEfo>> SendLoginUserAsync([FromBody, Required] RegisterUsersEfo registerUser)
        {
            RegisterUsersEfo loginUser = await _registerUsersService.SendLoginUserAsync(registerUser.Email, registerUser.Password);

            if (loginUser != null)
            {
                return StatusCode(StatusCodes.Status200OK, loginUser);
            }

            return StatusCode(StatusCodes.Status401Unauthorized);
        }

        // PUT api/registerusers/{id}/updatepassword
        [Authorize]
        [HttpPut("{id}/updatepassword")]
        [ProducesResponseType(typeof(RegisterUsersEfo), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> UpdatePasswordAsync(int id, string newPassword, string confirmPassword)
        {
            try
            {
                RegisterUsersEfo updatePassword = await _registerUsersService.UpdatePasswordAsync(id, newPassword, confirmPassword);

                if (updatePassword == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }

                return StatusCode(StatusCodes.Status201Created, updatePassword);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Internal server error: {ex.Message}");
            }
        }

        // DELETE api/registerusers/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> DeleteRegisterUserAsync(int id)
        {
            try
            {
                await _registerUsersService.DeleteRegisterUserAsync(id);

                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Internal server error: {ex.Message}");
            }
        }
    }
}
