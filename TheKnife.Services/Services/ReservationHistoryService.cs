using Microsoft.EntityFrameworkCore;
using TheKnife.Entities.Efos;
using TheKnife.EntityFramework;

namespace TheKnife.Services.Services
{
    public interface IReservationHistoryService
    {
        Task<List<ReservationHistoryEfo>> GetAllReservationHistoryAsync();
        Task<ReservationHistoryEfo> GetReservationHistoryByIdAsync(int id);
        Task DeleteReservationHistoryAsync(int id);
    }

    public class ReservationHistoryService : IReservationHistoryService
    {
        private readonly TheKnifeDbContext _context;

        public ReservationHistoryService(TheKnifeDbContext context)
        {
            _context = context;
        }

        public async Task<List<ReservationHistoryEfo>> GetAllReservationHistoryAsync()
        {
            return await _context.ReservationHistory.ToListAsync();
        }

        public async Task<ReservationHistoryEfo> GetReservationHistoryByIdAsync(int id)
        {
            ReservationHistoryEfo? reservationHistory = await _context.ReservationHistory.AsNoTracking()
                .FirstOrDefaultAsync(rh => rh.Id == id);

            if (reservationHistory == null)
            {
                throw new Exception("Entity doesn´t exist in the database!");
            }

            return reservationHistory;
        }

        public async Task DeleteReservationHistoryAsync(int id)
        {
            ReservationHistoryEfo? reservationHistory = await _context.ReservationHistory.FirstOrDefaultAsync(
                rh => rh.Id == id);

            if (reservationHistory == null)
            {
                throw new Exception("Entity doesn´t exist in the database!");
            }

            _context.ReservationHistory.Remove(reservationHistory);
            await _context.SaveChangesAsync();
        }
    }
}
