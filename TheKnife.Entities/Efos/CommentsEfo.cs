using System.ComponentModel.DataAnnotations.Schema;

namespace TheKnife.Entities.Efos
{
    public class CommentsEfo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public DateOnly Commentdate { get; set; }
        public decimal Review {  get; set; }
        public string Comment { get; set; } = string.Empty;

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int User_Id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Restaurant_Id { get; set; }

        public UsersEfo Users { get; set; }
        public RestaurantsEfo Restaurants { get; set; }
    }
}
