using Microsoft.EntityFrameworkCore;
using TheKnife.Entities.Efos;
using TheKnife.EntityFramework;

namespace TheKnife.Services.Services
{
    public interface IContactsService
    {
        Task<List<ContactsEfo>> GetAllContactsAsync();
        Task<ContactsEfo> GetContactByIdAsync(int id);
        Task<ContactsEfo> SendContactAsync(ContactsEfo contact);
        Task DeleteContactAsync(int id);
    }

    public class ContactsService : IContactsService
    {
        private readonly TheKnifeDbContext _context;

        public ContactsService(TheKnifeDbContext context)
        {
            _context = context;
        }

        public async Task<List<ContactsEfo>> GetAllContactsAsync()
        {
            return await _context.Contacts.ToListAsync();
        }

        public async Task<ContactsEfo> GetContactByIdAsync(int id)
        {
            ContactsEfo? contact = await _context.Contacts.AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);

            if (contact == null)
            {
                throw new Exception("Entity doesn´t exist in the database");
            }

            return contact;
        }

        public async Task<ContactsEfo> SendContactAsync(ContactsEfo contact)
        {
            await _context.Contacts.AddAsync(contact);
            await _context.SaveChangesAsync();

            return contact;
        }

        public async Task DeleteContactAsync(int id)
        {
            ContactsEfo? contact = await _context.Contacts.FirstOrDefaultAsync(
                c => c.Id == id);

            if (contact == null)
            {
                throw new Exception("Entity doesn´t exist in the database!");
            }

            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();
        }
    }
}
