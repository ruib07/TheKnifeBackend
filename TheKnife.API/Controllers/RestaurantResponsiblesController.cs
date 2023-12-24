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
    public class RestaurantResponsiblesController : ControllerBase
    {
        private readonly RestaurantResponsiblesService _restaurantResponsibles;

        public RestaurantResponsiblesController(RestaurantResponsiblesService restaurantResponsibles)
        {
            _restaurantResponsibles=restaurantResponsibles;
        }

        // GET api/restaurantresponsibles
        [HttpGet]
        [ProducesResponseType(typeof(List<RestaurantResponsiblesEfo>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<RestaurantResponsiblesEfo>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(List<RestaurantResponsiblesEfo>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(List<RestaurantResponsiblesEfo>), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<List<RestaurantResponsiblesEfo>>> GetAllRestaurantResponsiblesAsync()
        {
            List<RestaurantResponsiblesEfo> restaurantResponsibles = await _restaurantResponsibles
                .GetAllRestaurantResponsiblesAsync();

            if (restaurantResponsibles == null)
            {
                return StatusCode(StatusCodes.Status204NoContent);
            }

            return StatusCode(StatusCodes.Status200OK, restaurantResponsibles);
        }

        // GET api/restaurantresponsibles/{id}
        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(RestaurantResponsiblesEfo), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(RestaurantResponsiblesEfo), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(RestaurantResponsiblesEfo), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(RestaurantResponsiblesEfo), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetRestaurantResponsibleByIdAsync(int id)
        {
            RestaurantResponsiblesEfo restaurantResponsible = await _restaurantResponsibles
                .GetRestaurantResponsibleByIdAsync(id);

            if (restaurantResponsible == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            return StatusCode(StatusCodes.Status200OK, restaurantResponsible);
        }

        // POST api/restaurantresponsibles
        [HttpPost]
        [ProducesResponseType(typeof(RestaurantResponsiblesEfo), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(RestaurantResponsiblesEfo), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(RestaurantResponsiblesEfo), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(RestaurantResponsiblesEfo), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<RestaurantResponsiblesEfo>> SendRestaurantResponsible([FromBody, Required] RestaurantResponsiblesEfo restaurantResponsible)
        {
            if (ModelState.IsValid)
            {
                RestaurantResponsiblesEfo newRestaurantResponsible = await _restaurantResponsibles
                    .SendRestaurantResponsibleAsync(restaurantResponsible);

                return StatusCode(StatusCodes.Status201Created, newRestaurantResponsible);
            }

            return StatusCode(StatusCodes.Status400BadRequest, ModelState);
        }

        // POST api/restaurantresponsibles/sendlogin
        [HttpPost("sendlogin")]
        [ProducesResponseType(typeof(RestaurantResponsiblesEfo), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(RestaurantResponsiblesEfo), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(RestaurantResponsiblesEfo), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(RestaurantResponsiblesEfo), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(RestaurantResponsiblesEfo), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<RestaurantResponsiblesEfo>> SendLoginRestaurantResponsibleAsync([FromBody, Required] RestaurantResponsiblesEfo restaurantResponsible)
        {
            RestaurantResponsiblesEfo loginResponsible = await _restaurantResponsibles
                .SendLoginRestaurantResponsibleAsync(restaurantResponsible.Email, restaurantResponsible.Password);

            if (loginResponsible != null)
            {
                return StatusCode(StatusCodes.Status200OK, loginResponsible);
            }

            return StatusCode(StatusCodes.Status401Unauthorized);
        }

        // PUT api/restaurantresponsibles/{id}
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(RestaurantResponsiblesEfo), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(RestaurantResponsiblesEfo), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(RestaurantResponsiblesEfo), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(RestaurantResponsiblesEfo), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> UpdateRestaurantResponsibleAsync(int id, [FromBody] RestaurantResponsiblesEfo updateRestaurantResponsible)
        {
            try
            {
                RestaurantResponsiblesEfo restaurantResponsible = await _restaurantResponsibles
                    .UpdateRestaurantResponsibleAsync(id, updateRestaurantResponsible);

                if (restaurantResponsible == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }

                return StatusCode(StatusCodes.Status201Created, restaurantResponsible);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Internal server error: {ex.Message}");
            }
        }

        // DELETE api/restaurantresponsibles/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> DeleteRestaurantResponsibleAsync(int id)
        {
            try
            {
                await _restaurantResponsibles.DeleteRestaurantResponsibleAsync(id);

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
