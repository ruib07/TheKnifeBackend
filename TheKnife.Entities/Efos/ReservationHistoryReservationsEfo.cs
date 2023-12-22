using System.ComponentModel.DataAnnotations.Schema;

namespace TheKnife.Entities.Efos
{
    public class ReservationHistoryReservationsEfo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReservationHistory_Id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Reservation_Id { get; set; }

        public ReservationHistoryEfo ReservationHistory { get; set; }
        public ReservationsEfo Reservations { get; set; }
    }
}
