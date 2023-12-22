using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheKnife.Entities.Efos;

namespace TheKnife.EntityFramework.Configurations
{
    public class RestaurantsEfc : IEntityTypeConfiguration<RestaurantsEfo>
    {
        public void Configure(EntityTypeBuilder<RestaurantsEfo> builder)
        {
            builder.ToTable("Restaurants");
            builder.HasKey(property => new { property.Id });
            builder.Property(property => property.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Property(property => property.RName).IsRequired().HasMaxLength(50).IsUnicode(false);
            builder.Property(property => property.Category).IsRequired().HasMaxLength(50).IsUnicode(false);
            builder.Property(property => property.Desc).IsRequired().HasMaxLength(250).IsUnicode(false);
            builder.Property(property => property.Rphone).IsRequired();
            builder.Property(property => property.Location).IsRequired().HasMaxLength(150).IsUnicode(false);
            builder.Property(property => property.Image).IsRequired().HasMaxLength(400).IsUnicode(false);
            builder.Property(property => property.Numberoftables).IsRequired();
            builder.Property(property => property.Capacity).IsRequired();
            builder.Property(property => property.Openingdays).IsRequired().HasMaxLength(100).IsUnicode(false);
            builder.Property(property => property.Openinghours).IsRequired();
            builder.Property(property => property.Closinghours).IsRequired();
            builder.Property(property => property.RestaurantRegistration_Id).IsRequired().ValueGeneratedOnAdd();
            builder.Property(property => property.Rresponsible_Id).IsRequired().ValueGeneratedOnAdd();

            builder.HasOne(property => property.RestaurantRegistrations)
                .WithOne()
                .HasForeignKey<RestaurantsEfo>(property => property.RestaurantRegistration_Id)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(property => property.RestaurantResponsibles)
                .WithOne()
                .HasForeignKey<RestaurantsEfo>(property => property.Rresponsible_Id)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
