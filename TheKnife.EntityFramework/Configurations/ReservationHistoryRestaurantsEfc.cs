using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheKnife.Entities.Efos;

namespace TheKnife.EntityFramework.Configurations
{
    public class ReservationHistoryRestaurantsEfc : IEntityTypeConfiguration<ReservationHistoryRestaurantsEfo>
    {
        public void Configure(EntityTypeBuilder<ReservationHistoryRestaurantsEfo> builder)
        {
            builder.ToTable("ReservationHistoryRestaurants");
            builder.HasNoKey();
            builder.Property(property => property.ReservationHistory_Id).IsRequired().ValueGeneratedOnAdd();
            builder.Property(property => property.Restaurant_Id).IsRequired().ValueGeneratedOnAdd();

            builder.HasOne(property => property.ReservationHistory)
                .WithMany()
                .HasForeignKey(property => property.ReservationHistory_Id)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(property => property.Restaurants)
                .WithMany()
                .HasForeignKey(property => property.Restaurant_Id)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
