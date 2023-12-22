using Microsoft.EntityFrameworkCore;
using TheKnife.Entities.Efos;
using TheKnife.EntityFramework;

namespace TheKnife.Services.Services
{
    public interface IUsersService
    {
        Task<List<UsersEfo>> GetAllUsersAsync();
        Task<UsersEfo> GetUserByIdAsync(int id);
        Task<UsersEfo> SendUserAsync(UsersEfo user);
        Task<UsersEfo> UpdateUserAsync(int id, UsersEfo updateUser);
        Task DeleteUserAsync(int id);
    }

    public class UsersService : IUsersService
    {
        private readonly TheKnifeDbContext _context;

        public UsersService(TheKnifeDbContext context)
        {
            _context = context;
        }

        public async Task<List<UsersEfo>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<UsersEfo> GetUserByIdAsync(int id)
        {
            UsersEfo? user = await _context.Users.AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                throw new Exception("Entity doesn´t exist in the database!");
            }

            return user;
        }

        public async Task<UsersEfo> SendUserAsync(UsersEfo user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<UsersEfo> UpdateUserAsync(int id, UsersEfo updateUser)
        {
            try
            {
                UsersEfo? user = await _context.Users.FindAsync(id);

                if (user == null)
                {
                    throw new Exception("Entity doesn´t exist in the database!");
                }

                user.UserName = updateUser.UserName;
                user.Email = updateUser.Email;
                user.Password = updateUser.Password;
                user.Image = updateUser.Image;

                await _context.SaveChangesAsync();

                return user;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating user: {ex.Message}");
            }
        }

        public async Task DeleteUserAsync(int id)
        {
            UsersEfo? user = await _context.Users.FirstOrDefaultAsync(
                u => u.Id == id);

            if (user == null )
            {
                throw new Exception("Entity doesn´t exist in the database!");
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}
