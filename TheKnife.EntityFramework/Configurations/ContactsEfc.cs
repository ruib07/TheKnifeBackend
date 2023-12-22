using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheKnife.Entities.Efos;

namespace TheKnife.EntityFramework.Configurations
{
    public class ContactsEfc : IEntityTypeConfiguration<ContactsEfo>
    {
        public void Configure(EntityTypeBuilder<ContactsEfo> builder)
        {
            builder.ToTable("Contacts");
            builder.HasKey(property => new { property.Id });
            builder.Property(property => property.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Property(property => property.Client_Name).IsRequired().HasMaxLength(50).IsUnicode(false);
            builder.Property(property => property.Email).IsRequired().HasMaxLength(50).IsUnicode(false);
            builder.Property(property => property.PhoneNumber).IsRequired();
            builder.Property(property => property.Subject).IsRequired().HasMaxLength(50).IsUnicode(false);
            builder.Property(property => property.Message).IsRequired().HasMaxLength(250).IsUnicode(false);
        }
    }
}
