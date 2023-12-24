using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using TheKnife.Entities.Efos;
using TheKnife.Services.Services;

namespace TheKnife.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationHistoryController : ControllerBase
    {
        private readonly ReservationHistoryService _reservationHistoryService;

        public ReservationHistoryController(ReservationHistoryService reservationHistoryService)
        {
            _reservationHistoryService=reservationHistoryService;
        }

        // GET api/reservationhistory
        [HttpGet]
        [ProducesResponseType(typeof(List<ReservationHistoryEfo>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<ReservationHistoryEfo>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(List<ReservationHistoryEfo>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(List<ReservationHistoryEfo>), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<List<ReservationHistoryEfo>>> GetAllReservationHistoryAsync()
        {
            List<ReservationHistoryEfo> reservationHistories = await _reservationHistoryService.GetAllReservationHistoryAsync();

            if (reservationHistories == null)
            {
                return StatusCode(StatusCodes.Status204NoContent);
            }

            return StatusCode(StatusCodes.Status200OK, reservationHistories);
        }

        // GET api/reservationhistory/{id}
        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ReservationHistoryEfo), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ReservationHistoryEfo), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ReservationHistoryEfo), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ReservationHistoryEfo), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetReservationHistoryByIdAsync(int id)
        {
            ReservationHistoryEfo reservationHistory = await _reservationHistoryService.GetReservationHistoryByIdAsync(id);

            if (reservationHistory == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            return StatusCode(StatusCodes.Status200OK, reservationHistory);
        }

        // DELETE api/reservationhistory/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> DeleteReservationHistoryAsync(int id)
        {
            try
            {
                await _reservationHistoryService.DeleteReservationHistoryAsync(id);

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
