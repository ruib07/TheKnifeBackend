using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheKnife.Entities.Efos;

namespace TheKnife.EntityFramework.Configurations
{
    public class ReservationHistoryReservationsEfc : IEntityTypeConfiguration<ReservationHistoryReservationsEfo>
    {
        public void Configure(EntityTypeBuilder<ReservationHistoryReservationsEfo> builder)
        {
            builder.ToTable("ReservationHistoryReservations");
            builder.HasNoKey();
            builder.Property(property => property.ReservationHistory_Id).IsRequired().ValueGeneratedOnAdd();
            builder.Property(property => property.Reservation_Id).IsRequired().ValueGeneratedOnAdd();

            builder.HasOne(property => property.ReservationHistory)
                .WithMany()
                .HasForeignKey(property => property.ReservationHistory_Id)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(property => property.Reservations)
                .WithMany()
                .HasForeignKey(property => property.Reservation_Id)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
