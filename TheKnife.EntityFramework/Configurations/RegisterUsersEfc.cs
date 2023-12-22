using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheKnife.Entities.Efos;

namespace TheKnife.EntityFramework.Configurations
{
    public class RegisterUsersEfc : IEntityTypeConfiguration<RegisterUsersEfo>
    {
        public void Configure(EntityTypeBuilder<RegisterUsersEfo> builder)
        {
            builder.ToTable("RegisterUsers");
            builder.HasKey(property => new { property.Id });
            builder.Property(property => property.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Property(property => property.UserName).IsRequired().HasMaxLength(50).IsUnicode(false);
            builder.Property(property => property.Email).IsRequired().HasMaxLength(50).IsUnicode(false);
            builder.Property(property => property.Password).IsRequired().HasMaxLength(16).IsUnicode(false);
        }
    }
}
