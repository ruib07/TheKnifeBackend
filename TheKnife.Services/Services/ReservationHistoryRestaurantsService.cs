using Microsoft.EntityFrameworkCore;
using TheKnife.Entities.Efos;
using TheKnife.EntityFramework;

namespace TheKnife.Services.Services
{
    public interface IReservationHistoryRestaurantsService
    {
        Task<List<ReservationHistoryRestaurantsEfo>> GetAllReservationHistoryRestaurantsAsync();
    }

    public class ReservationHistoryRestaurantsService : IReservationHistoryRestaurantsService
    {
        private readonly TheKnifeDbContext _context;

        public ReservationHistoryRestaurantsService(TheKnifeDbContext context)
        {
            _context = context;
        }

        public async Task<List<ReservationHistoryRestaurantsEfo>> GetAllReservationHistoryRestaurantsAsync()
        {
            return await _context.ReservationHistoryRestaurants.ToListAsync();
        }
    }
}
