using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheKnife.Entities.Efos;

namespace TheKnife.EntityFramework.Configurations
{
    public class RestaurantResponsiblesEfc : IEntityTypeConfiguration<RestaurantResponsiblesEfo>
    {
        public void Configure(EntityTypeBuilder<RestaurantResponsiblesEfo> builder)
        {
            builder.ToTable("RestaurantResponsibles");
            builder.HasKey(property => property.Id);
            builder.Property(property => property.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Property(property => property.FlName).IsRequired().HasMaxLength(50).IsUnicode(false);
            builder.Property(property => property.Phone).IsRequired();
            builder.Property(property => property.Email).IsRequired().HasMaxLength(50).IsUnicode(false);
            builder.Property(property => property.Password).IsRequired().HasMaxLength(16).IsUnicode(false);
            builder.Property(property => property.RestaurantRegistration_Id).IsRequired().ValueGeneratedOnAdd();

            builder.HasOne(property => property.RestaurantRegistrations)
                .WithOne()
                .HasForeignKey<RestaurantResponsiblesEfo>(property => property.RestaurantRegistration_Id)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
