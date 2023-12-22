using Microsoft.EntityFrameworkCore;
using TheKnife.Entities.Efos;
using TheKnife.EntityFramework;

namespace TheKnife.Services.Services
{
    public interface IRegisterUsersService
    {
        Task<List<RegisterUsersEfo>> GetAllRegisterUsersAsync();
        Task<RegisterUsersEfo> GetRegisterUserByIdAsync(int id);
        Task<RegisterUsersEfo> SendRegisterUserAsync(RegisterUsersEfo registerUser);
        Task<RegisterUsersEfo> SendLoginUserAsync(string email, string password);
        Task<RegisterUsersEfo> RecoverPasswordAsync(int id, string newPassword, string confirmPassword);
        Task DeleteRegisterUserAsync(int id);
    }

    public class RegisterUsersService : IRegisterUsersService
    {
        private readonly TheKnifeDbContext _context;

        public RegisterUsersService(TheKnifeDbContext context)
        {
            _context = context;
        }

        public async Task<List<RegisterUsersEfo>> GetAllRegisterUsersAsync()
        {
            return await _context.RegisterUsers.ToListAsync();
        }

        public async Task<RegisterUsersEfo> GetRegisterUserByIdAsync(int id)
        {
            RegisterUsersEfo? registerUser = await _context.RegisterUsers.AsNoTracking()
                .FirstOrDefaultAsync(ru => ru.Id == id);

            if (registerUser == null)
            {
                throw new Exception("Entity doesn´t exist in the database!");
            }

            return registerUser;
        }

        public async Task<RegisterUsersEfo> SendRegisterUserAsync(RegisterUsersEfo registerUser)
        {
            await _context.RegisterUsers.AddAsync(registerUser);
            await _context.SaveChangesAsync();

            return registerUser;
        }

        public async Task<RegisterUsersEfo> SendLoginUserAsync(string email, string password)
        {
            RegisterUsersEfo? loginUser = await _context.RegisterUsers.FirstOrDefaultAsync(
                lu => lu.Email == email && lu.Password == password);

            return loginUser;
        }

        public async Task<RegisterUsersEfo> RecoverPasswordAsync(int id, string newPassword, string confirmPassword)
        {
            RegisterUsersEfo? updatePasswordRegisterUser = await _context.RegisterUsers.FirstOrDefaultAsync(
                ru => ru.Id == id);

            if (updatePasswordRegisterUser == null)
            {
                throw new Exception("Entity doesn´t exist in the database!");
            }

            if (newPassword != confirmPassword)
            {
                throw new Exception("Passwords must be equal!");
            }

            updatePasswordRegisterUser.Password = newPassword;

            await _context.SaveChangesAsync();

            UsersEfo? updatePassInUserTable = await _context.Users.FirstOrDefaultAsync(
                pu => pu.Id == id);

            if (updatePassInUserTable == null)
            {
                throw new Exception("Entity doesn´t exist in the database!");
            }

            await _context.SaveChangesAsync();
            return updatePasswordRegisterUser;
        }

        public async Task DeleteRegisterUserAsync(int id)
        {
            RegisterUsersEfo? registerUser = await _context.RegisterUsers.FirstOrDefaultAsync(
                ru => ru.Id == id);

            if (registerUser == null)
            {
                throw new Exception("Entity doesn´t exist in the database!");
            }

            _context.RegisterUsers.Remove(registerUser);
            await _context.SaveChangesAsync();
        }
    }
}
