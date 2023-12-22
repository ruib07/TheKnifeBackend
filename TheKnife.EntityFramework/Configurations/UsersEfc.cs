using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheKnife.Entities.Efos;

namespace TheKnife.EntityFramework.Configurations
{
    public class UsersEfc : IEntityTypeConfiguration<UsersEfo>
    {
        public void Configure(EntityTypeBuilder<UsersEfo> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(property => new { property.Id });
            builder.Property(property => property.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Property(property => property.UserName).IsRequired().HasMaxLength(50).IsUnicode(false);
            builder.Property(property => property.Email).IsRequired().HasMaxLength(50).IsUnicode(false);
            builder.Property(property => property.Password).IsRequired().HasMaxLength(16).IsUnicode(false);
            builder.Property(property => property.Image).HasMaxLength(400).IsUnicode(false);
            builder.Property(property => property.RegisterUser_Id).IsRequired().ValueGeneratedOnAdd();

            builder.HasOne(property => property.RegisterUsers)
                .WithOne()
                .HasForeignKey<UsersEfo>(property => property.RegisterUser_Id)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
