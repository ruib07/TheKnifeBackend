using System.ComponentModel.DataAnnotations.Schema;

namespace TheKnife.Entities.Efos
{
    public class ReservationHistoryEfo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateOnly Reservationdate { get; set; }
        public TimeOnly Reservationtime { get; set; }
        public string Client_Name { get; set; } = string.Empty;
        public string Restaurant_Name { get; set; } = string.Empty;

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int User_Id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Restaurant_Id { get; set; }

        public UsersEfo Users { get; set; }
        public RestaurantsEfo Restaurants { get; set; }
    }
}
