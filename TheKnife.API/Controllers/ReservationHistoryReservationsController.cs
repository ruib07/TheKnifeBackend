using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using TheKnife.Entities.Efos;
using TheKnife.Services.Services;

namespace TheKnife.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationHistoryReservationsController : ControllerBase
    {
        private readonly ReservationHistoryReservationsService _reservationHistoryReservationsService;

        public ReservationHistoryReservationsController(ReservationHistoryReservationsService reservationHistoryReservationsService)
        {
            _reservationHistoryReservationsService = reservationHistoryReservationsService;
        }

        // GET api/reservationhistoryreservations
        [HttpGet]
        [ProducesResponseType(typeof(List<ReservationHistoryReservationsEfo>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<ReservationHistoryReservationsEfo>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(List<ReservationHistoryReservationsEfo>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(List<ReservationHistoryReservationsEfo>), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<List<ReservationHistoryReservationsEfo>>> GetAllReservationHistoryReservationsAsync()
        {
            List<ReservationHistoryReservationsEfo> reservationHistoryReservations = await _reservationHistoryReservationsService
                .GetAllReservationHistoryReservationsAsync();

            if (reservationHistoryReservations == null)
            {
                return StatusCode(StatusCodes.Status204NoContent);
            }

            return StatusCode(StatusCodes.Status200OK, reservationHistoryReservations);
        }
    }
}
