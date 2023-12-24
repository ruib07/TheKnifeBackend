using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using TheKnife.Entities.Efos;
using TheKnife.Services.Services;

namespace TheKnife.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationHistoryRestaurantsController : ControllerBase
    {
        private readonly ReservationHistoryRestaurantsService _reservationHistoryRestaurantsService;

        public ReservationHistoryRestaurantsController(ReservationHistoryRestaurantsService reservationHistoryRestaurantssService)
        {
            _reservationHistoryRestaurantsService = reservationHistoryRestaurantssService;
        }

        // GET api/reservationhistoryrestaurants
        [HttpGet]
        [ProducesResponseType(typeof(List<ReservationHistoryRestaurantsEfo>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<ReservationHistoryRestaurantsEfo>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(List<ReservationHistoryRestaurantsEfo>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(List<ReservationHistoryRestaurantsEfo>), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<List<ReservationHistoryRestaurantsEfo>>> GetAllReservationHistoryReservationsAsync()
        {
            List<ReservationHistoryRestaurantsEfo> reservationHistoryRestaurants = await _reservationHistoryRestaurantsService
                .GetAllReservationHistoryRestaurantsAsync();

            if (reservationHistoryRestaurants == null)
            {
                return StatusCode(StatusCodes.Status204NoContent);
            }

            return StatusCode(StatusCodes.Status200OK, reservationHistoryRestaurants);
        }
    }
}
