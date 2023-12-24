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
    public class RestaurantRegistrationsController : ControllerBase
    {
        private readonly RestaurantResgistrationsService _restaurantRegistrations;

        public RestaurantRegistrationsController(RestaurantResgistrationsService restaurantRegistrations)
        {
            _restaurantRegistrations=restaurantRegistrations;
        }

        // GET api/restaurantregistrations
        [HttpGet]
        [ProducesResponseType(typeof(List<RestaurantRegistrationsEfo>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<RestaurantRegistrationsEfo>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(List<RestaurantRegistrationsEfo>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(List<RestaurantRegistrationsEfo>), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<List<RestaurantRegistrationsEfo>>> GetAllRestaurantRegistrationsAsync()
        {
            List<RestaurantRegistrationsEfo> restaurantRegistrations = await _restaurantRegistrations
                .GetAllRestaurantRegistrationsAsync();

            if (restaurantRegistrations == null)
            {
                return StatusCode(StatusCodes.Status204NoContent);
            }

            return StatusCode(StatusCodes.Status200OK, restaurantRegistrations);
        }

        // GET api/restaurantregistrations/{id}
        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(RestaurantRegistrationsEfo), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(RestaurantRegistrationsEfo), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(RestaurantRegistrationsEfo), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(RestaurantRegistrationsEfo), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetRestaurantRegistrationByIdAsync(int id)
        {
            RestaurantRegistrationsEfo restaurantRegistration = await _restaurantRegistrations
                .GetRestaurantRegistrationByIdAsync(id);

            if (restaurantRegistration == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            return StatusCode(StatusCodes.Status200OK, restaurantRegistration);
        }

        // POST api/restaurantregistrations
        [HttpPost]
        [ProducesResponseType(typeof(RestaurantRegistrationsEfo), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(RestaurantRegistrationsEfo), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(RestaurantRegistrationsEfo), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(RestaurantRegistrationsEfo), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<RestaurantRegistrationsEfo>> SendRestaurantRegistrationAsync([FromBody, Required] RestaurantRegistrationsEfo restaurant)
        {
            if (ModelState.IsValid)
            {
                RestaurantRegistrationsEfo newRestaurantRegistration = await _restaurantRegistrations
                    .SendRestaurantRegistrationAsync(restaurant);

                return StatusCode(StatusCodes.Status201Created, newRestaurantRegistration);
            }

            return StatusCode(StatusCodes.Status400BadRequest, ModelState);
        }

        // PUT api/restaurantregistrations/{id}/updatepassword
        [Authorize]
        [HttpPut("{id}/updatepassword")]
        [ProducesResponseType(typeof(RestaurantRegistrationsEfo), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> UpdatePasswordAsync(int id, string newPassword, string confirmPassword)
        {
            try
            {
                RestaurantRegistrationsEfo updatePassword = await _restaurantRegistrations.UpdatePasswordAsync(id, newPassword, confirmPassword);

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

        // PUT api/restaurantregistrations/{id}
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(RestaurantRegistrationsEfo), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(RestaurantRegistrationsEfo), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(RestaurantRegistrationsEfo), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(RestaurantRegistrationsEfo), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> UpdateRestaurantRegistrationAsync(int id, [FromBody] RestaurantRegistrationsEfo updateRestaurantRegistration)
        {
            try
            {
                RestaurantRegistrationsEfo restaurantRegistration = await _restaurantRegistrations
                    .UpdateRestaurantRegistrationAsync(id, updateRestaurantRegistration);

                if (restaurantRegistration == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }

                return StatusCode(StatusCodes.Status201Created, restaurantRegistration);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Internal server error: {ex.Message}");
            }
        }

        // DELETE api/restaurantregistrations/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> DeleteRestaurantRegistrationAsync(int id)
        {
            try
            {
                await _restaurantRegistrations.DeleteRestaurantRegistrationAsync(id);

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
