using Microsoft.EntityFrameworkCore;
using TheKnife.Entities.Efos;
using TheKnife.EntityFramework;

namespace TheKnife.Services.Services
{
    public interface IReservationsService
    {
        Task<List<ReservationsEfo>> GetAllReservationsAsync();
        Task<ReservationsEfo> GetReservationByIdAsync(int id);
        Task<ReservationsEfo> SendReservationAsync(ReservationsEfo reservation);
        Task<ReservationsEfo> UpdateReservationAsync(int id, ReservationsEfo updateReservation);
        Task DeleteReservationAync(int id);
    }
    public class ReservationsService : IReservationsService
    {
        private readonly TheKnifeDbContext _context;

        public ReservationsService(TheKnifeDbContext context)
        {
            _context = context;
        }

        public async Task<List<ReservationsEfo>> GetAllReservationsAsync()
        {
            return await _context.Reservations.ToListAsync();
        }

        public async Task<ReservationsEfo> GetReservationByIdAsync(int id)
        {
            ReservationsEfo? reservation = await _context.Reservations.AsNoTracking()
                .FirstOrDefaultAsync(r => r.Id == id);

            if (reservation == null)
            {
                throw new Exception("Entity doesn´t exist in the database!");
            }

            return reservation;
        }

        public async Task<ReservationsEfo> SendReservationAsync(ReservationsEfo reservation)
        {
            await _context.Reservations.AddAsync(reservation);
            await _context.SaveChangesAsync();

            return reservation;
        }

        public async Task<ReservationsEfo> UpdateReservationAsync(int id, ReservationsEfo updateReservation)
        {
            try
            {
                ReservationsEfo? reservation = await _context.Reservations.FindAsync(id);

                if (reservation == null )
                {
                    throw new Exception("Entity doesn´t exist in the database!");
                }

                reservation.Client_Name = updateReservation.Client_Name;
                reservation.PhoneNumber = updateReservation.PhoneNumber;
                reservation.Reservationdate = updateReservation.Reservationdate;
                reservation.Reservationtime = updateReservation.Reservationtime;
                reservation.Numberofpeople = updateReservation.Numberofpeople;

                await _context.SaveChangesAsync();

                return reservation;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating reservation: {ex.Message}");
            }
        }

        public async Task DeleteReservationAync(int id)
        {
            ReservationsEfo? reservation = await _context.Reservations.FirstOrDefaultAsync(
                r => r.Id == id);

            if (reservation == null )
            {
                throw new Exception("Entity doesn´t exist in the database!");
            }

            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();
        }
    }
}
