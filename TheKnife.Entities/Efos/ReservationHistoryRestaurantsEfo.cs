using System.ComponentModel.DataAnnotations.Schema;

namespace TheKnife.Entities.Efos
{
    public class ReservationHistoryRestaurantsEfo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReservationHistory_Id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Restaurant_Id { get; set; }

        public ReservationHistoryEfo ReservationHistory {  get; set; }
        public RestaurantsEfo Restaurants { get; set; }
    }
}
