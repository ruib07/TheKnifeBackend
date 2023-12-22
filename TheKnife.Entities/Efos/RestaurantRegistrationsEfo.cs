using System.ComponentModel.DataAnnotations.Schema;

namespace TheKnife.Entities.Efos
{
    public class RestaurantRegistrationsEfo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string FlName { get; set; } = string.Empty;
        public int Phone {  get; set; }
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string RName { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Desc { get; set; } = string.Empty;
        public int Rphone { get; set; }
        public string Location { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public int Numberoftables { get; set; }
        public int Capacity { get; set; }
        public string Openingdays { get; set; } = string.Empty;
        public TimeOnly Openinghours { get; set; }
        public TimeOnly Closinghours { get; set; }
    }
}
