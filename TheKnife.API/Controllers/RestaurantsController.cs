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
    public class RestaurantsController : ControllerBase
    {
        private readonly RestaurantsService _restaurantsService;

        public RestaurantsController(RestaurantsService restaurantsService)
        {
            _restaurantsService = restaurantsService;
        }

        // GET api/restaurants
        [HttpGet]
        [ProducesResponseType(typeof(List<RestaurantsEfo>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<RestaurantsEfo>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(List<RestaurantsEfo>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(List<RestaurantsEfo>), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<List<RestaurantsEfo>>> GetAllRestaurantsAsync()
        {
            List<RestaurantsEfo> restaurants = await _restaurantsService.GetAllRestaurantsAsync();

            if (restaurants == null)
            {
                return StatusCode(StatusCodes.Status204NoContent);
            }

            return StatusCode(StatusCodes.Status200OK, restaurants);
        }

        // GET api/restaurants/{id}
        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(RestaurantsEfo), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(RestaurantsEfo), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(RestaurantsEfo), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(RestaurantsEfo), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetRestaurantByIdAsync(int id)
        {
            RestaurantsEfo restaurant = await _restaurantsService.GetRestaurantByIdAsync(id);

            if (restaurant == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            return StatusCode(StatusCodes.Status200OK, restaurant);
        }

        // POST api/restaurants
        [HttpPost]
        [ProducesResponseType(typeof(RestaurantsEfo), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(RestaurantsEfo), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(RestaurantsEfo), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(RestaurantsEfo), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<RestaurantsEfo>> SendRestaurantAsync([FromBody, Required] RestaurantsEfo restaurant)
        {
            if (ModelState.IsValid)
            {
                RestaurantsEfo newRestaurant = await _restaurantsService.SendRestaurantAsync(restaurant);
                return StatusCode(StatusCodes.Status201Created, newRestaurant);
            }

            return StatusCode(StatusCodes.Status400BadRequest, ModelState);
        }

        // PUT api/restaurants/{id}
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(RestaurantsEfo), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(RestaurantsEfo), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(RestaurantsEfo), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(RestaurantsEfo), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> UpdateRestaurantAsync(int id, [FromBody] RestaurantsEfo updateRestaurant)
        {
            try
            {
                RestaurantsEfo restaurant = await _restaurantsService.UpdateRestaurantAsync(id, updateRestaurant);

                if (restaurant == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }

                return StatusCode(StatusCodes.Status201Created, restaurant);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Internal server error: {ex.Message}");
            }
        }

        // DELETE api/restaurants/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> DeleteRestaurantAsync(int id)
        {
            try
            {
                await _restaurantsService.DeleteRestaurantAsync(id);

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
