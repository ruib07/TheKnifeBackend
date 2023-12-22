using Microsoft.EntityFrameworkCore;
using TheKnife.Entities.Efos;
using TheKnife.EntityFramework;

namespace TheKnife.Services.Services
{
    public interface IReservationHistoryReservationsService
    {
        Task<List<ReservationHistoryReservationsEfo>> GetAllReservationHistoryReservationsAsync();
    }

    public class ReservationHistoryReservationsService : IReservationHistoryReservationsService
    {
        private readonly TheKnifeDbContext _context;

        public ReservationHistoryReservationsService(TheKnifeDbContext context)
        {
            _context = context;
        }

        public async Task<List<ReservationHistoryReservationsEfo>> GetAllReservationHistoryReservationsAsync()
        {
            return await _context.ReservationHistoryReservations.ToListAsync();
        }
    }
}
