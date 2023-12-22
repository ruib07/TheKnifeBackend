using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheKnife.Entities.Efos;

namespace TheKnife.EntityFramework.Configurations
{
    public class ReservationsEfc : IEntityTypeConfiguration<ReservationsEfo>
    {
        public void Configure(EntityTypeBuilder<ReservationsEfo> builder)
        {
            builder.ToTable("Reservations");
            builder.HasKey(property => new { property.Id });
            builder.Property(property => property.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Property(property => property.Client_Name).IsRequired().HasMaxLength(50).IsUnicode(false);
            builder.Property(property => property.PhoneNumber).IsRequired();
            builder.Property(property => property.Reservationdate).IsRequired();
            builder.Property(property => property.Reservationtime).IsRequired();
            builder.Property(property => property.Numberofpeople).IsRequired();
            builder.Property(property => property.Restaurant_Id).IsRequired().ValueGeneratedOnAdd();
            builder.Property(property => property.User_Id).IsRequired().ValueGeneratedOnAdd();

            builder.HasOne(property => property.Restaurants)
                .WithMany()
                .HasForeignKey(property => property.Restaurant_Id)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(property => property.Users)
                .WithMany()
                .HasForeignKey(property => property.User_Id)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
