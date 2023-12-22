using System.ComponentModel.DataAnnotations.Schema;

namespace TheKnife.Entities.Efos
{
    public class ReservationsEfo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Client_Name { get; set; } = string.Empty;
        public int PhoneNumber { get; set; }
        public DateOnly Reservationdate { get; set; }
        public TimeOnly Reservationtime { get; set; }
        public int Numberofpeople { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Restaurant_Id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int User_Id { get; set; }

        public RestaurantsEfo Restaurants { get; set; }
        public UsersEfo Users { get; set; }
    }
}
