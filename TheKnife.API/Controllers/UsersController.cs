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
    public class UsersController : ControllerBase
    {
        private readonly UsersService _usersService;

        public UsersController(UsersService usersService)
        {
            _usersService = usersService;
        }

        // GET api/users
        [HttpGet]
        [ProducesResponseType(typeof(List<UsersEfo>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<UsersEfo>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(List<UsersEfo>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(List<UsersEfo>), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<List<UsersEfo>>> GetAllUsersAsync()
        {
            List<UsersEfo> users = await _usersService.GetAllUsersAsync();

            if (users == null)
            {
                return StatusCode(StatusCodes.Status204NoContent);
            }

            return StatusCode(StatusCodes.Status200OK, users);
        }

        // GET api/users/{id}
        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UsersEfo), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UsersEfo), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(UsersEfo), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(UsersEfo), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetUserByIdAsync(int id)
        {
            UsersEfo user = await _usersService.GetUserByIdAsync(id);

            if (user == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            return StatusCode(StatusCodes.Status200OK, user);
        }

        // POST api/users
        [HttpPost]
        [ProducesResponseType(typeof(UsersEfo), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(UsersEfo), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(UsersEfo), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(UsersEfo), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<UsersEfo>> SendUserAsync([FromBody, Required] UsersEfo user)
        {
            if (ModelState.IsValid)
            {
                UsersEfo newUser = await _usersService.SendUserAsync(user);
                return StatusCode(StatusCodes.Status201Created, newUser);
            }

            return StatusCode(StatusCodes.Status400BadRequest, ModelState);
        }

        // PUT api/users/{id}
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(UsersEfo), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(UsersEfo), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(UsersEfo), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(UsersEfo), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> UpdateUserAsync(int id, [FromBody] UsersEfo updateUser)
        {
            try
            {
                UsersEfo user = await _usersService.UpdateUserAsync(id, updateUser);

                if (user == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }

                return StatusCode(StatusCodes.Status201Created, user);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Internal server error: {ex.Message}");
            }
        }

        // DELETE api/users/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> DeleteUserAsync(int id)
        {
            try
            {
                await _usersService.DeleteUserAsync(id);

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
