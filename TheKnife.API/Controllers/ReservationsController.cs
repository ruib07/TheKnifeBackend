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
    public class ReservationsController : ControllerBase
    {
        private readonly ReservationsService _reservationsService;

        public ReservationsController(ReservationsService reservationsService)
        {
            _reservationsService = reservationsService;
        }

        // GET api/reservations
        [HttpGet]
        [ProducesResponseType(typeof(List<ReservationsEfo>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<ReservationsEfo>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(List<ReservationsEfo>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(List<ReservationsEfo>), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<List<ReservationsEfo>>> GetAllReservationsAsync()
        {
            List<ReservationsEfo> reservations = await _reservationsService.GetAllReservationsAsync();

            if (reservations == null)
            {
                return StatusCode(StatusCodes.Status204NoContent);
            }

            return StatusCode(StatusCodes.Status200OK, reservations);
        }

        // GET api/reservations/{id}
        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ReservationsEfo), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ReservationsEfo), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ReservationsEfo), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ReservationsEfo), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetReservationByIdAsync(int id)
        {
            ReservationsEfo reservation = await _reservationsService.GetReservationByIdAsync(id);

            if (reservation == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            return StatusCode(StatusCodes.Status200OK, reservation);
        }

        // POST api/reservations
        [HttpPost]
        [ProducesResponseType(typeof(ReservationsEfo), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ReservationsEfo), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ReservationsEfo), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ReservationsEfo), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<ReservationsEfo>> SendReservationAsync([FromBody, Required] ReservationsEfo reservation)
        {
            if (ModelState.IsValid)
            {
                ReservationsEfo newReservation = await _reservationsService.SendReservationAsync(reservation);

                return StatusCode(StatusCodes.Status201Created, newReservation);
            }

            return StatusCode(StatusCodes.Status400BadRequest, ModelState);
        }

        // PUT api/reservations/{id}
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ReservationsEfo), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ReservationsEfo), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ReservationsEfo), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ReservationsEfo), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> UpdateReservationAsync(int id, [FromBody] ReservationsEfo updateReservation)
        {
            try
            {
                ReservationsEfo reservation = await _reservationsService.UpdateReservationAsync(id, updateReservation);

                if (reservation == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }

                return StatusCode(StatusCodes.Status201Created, reservation);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Internal server error: {ex.Message}");
            }
        }

        // DELETE api/reservations/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> DeleteReservationAsync(int id)
        {
            try
            {
                await _reservationsService.DeleteReservationAync(id);

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
