using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheKnife.Entities.Efos;

namespace TheKnife.EntityFramework.Configurations
{
    public class CommentsEfc : IEntityTypeConfiguration<CommentsEfo>
    {
        public void Configure(EntityTypeBuilder<CommentsEfo> builder)
        {
            builder.ToTable("Comments");
            builder.HasKey(property => new { property.Id });
            builder.Property(property => property.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Property(property => property.UserName).IsRequired().HasMaxLength(50).IsUnicode(false);
            builder.Property(property => property.Commentdate).IsRequired();
            builder.Property(property => property.Review).IsRequired().HasColumnType("decimal(3,1)");
            builder.Property(property => property.Comment).IsRequired().HasMaxLength(250).IsUnicode(false);
            builder.Property(property => property.User_Id).IsRequired().ValueGeneratedOnAdd();
            builder.Property(property => property.Restaurant_Id).IsRequired().ValueGeneratedOnAdd();

            builder.HasOne(property => property.Users)
                .WithMany()
                .HasForeignKey(property => property.User_Id)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(property => property.Restaurants)
                .WithMany()
                .HasForeignKey(property => property.Restaurant_Id)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
