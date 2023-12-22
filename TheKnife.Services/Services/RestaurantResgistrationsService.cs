using Microsoft.EntityFrameworkCore;
using TheKnife.Entities.Efos;
using TheKnife.EntityFramework;

namespace TheKnife.Services.Services
{
    public interface IRestaurantRegistrationsService
    {
        Task<List<RestaurantRegistrationsEfo>> GetAllRestaurantRegistrationsAsync();
        Task<RestaurantRegistrationsEfo> GetRestaurantRegistrationByIdAsync(int id);
        Task<RestaurantRegistrationsEfo> SendRestaurantRegistrationAsync(RestaurantRegistrationsEfo restaurantRegistration);
        Task<RestaurantRegistrationsEfo> UpdatePasswordAsync(int id, string newPassword, string confirmPassword);
        Task<RestaurantRegistrationsEfo> UpdateRestaurantRegistrationAsync(int id, RestaurantRegistrationsEfo updateRestaurantRegistration);
        Task DeleteRestaurantRegistrationAsync(int id);
    }

    public class RestaurantResgistrationsService : IRestaurantRegistrationsService
    {
        private readonly TheKnifeDbContext _context;

        public RestaurantResgistrationsService(TheKnifeDbContext context)
        {
            _context = context;
        }

        public async Task<List<RestaurantRegistrationsEfo>> GetAllRestaurantRegistrationsAsync()
        {
            return await _context.RestaurantRegistrations.ToListAsync();
        }

        public async Task<RestaurantRegistrationsEfo> GetRestaurantRegistrationByIdAsync(int id)
        {
            RestaurantRegistrationsEfo? restaurantRegistration = await _context.RestaurantRegistrations.AsNoTracking()
                .FirstOrDefaultAsync(rr => rr.Id == id);

            if (restaurantRegistration == null)
            {
                throw new Exception("Entity doesn´t exist in the database!");
            }

            return restaurantRegistration;
        }

        public async Task<RestaurantRegistrationsEfo> SendRestaurantRegistrationAsync(RestaurantRegistrationsEfo restaurantRegistration)
        {
            await _context.RestaurantRegistrations.AddAsync(restaurantRegistration);
            await _context.SaveChangesAsync();

            return restaurantRegistration;
        }

        public async Task<RestaurantRegistrationsEfo> UpdatePasswordAsync(int id, string newPassword, string confirmPassword)
        {
            RestaurantRegistrationsEfo? updatePasswordRRegistration = await _context.RestaurantRegistrations.FirstOrDefaultAsync(
                rr => rr.Id == id);

            if (updatePasswordRRegistration == null)
            {
                throw new Exception("Entity doesn´t exist in the database!");
            }

            if (newPassword != confirmPassword)
            {
                throw new Exception("Passwords must be equal!");
            }

            updatePasswordRRegistration.Password = newPassword;

            await _context.SaveChangesAsync();

            RestaurantResponsiblesEfo? updatePassRResponsibleTable = await _context.RestaurantResponsibles.FirstOrDefaultAsync(
                pr => pr.Id == id);

            if (updatePassRResponsibleTable == null)
            {
                throw new Exception("Entity doesn´t exist in the database!");
            }

            await _context.SaveChangesAsync();
            return updatePasswordRRegistration;
        }

        public async Task<RestaurantRegistrationsEfo> UpdateRestaurantRegistrationAsync(int id, RestaurantRegistrationsEfo updateRestaurantRegistration)
        {
            try
            {
                RestaurantRegistrationsEfo? restaurantRegistration = await _context.RestaurantRegistrations.FindAsync(id);

                if (restaurantRegistration == null)
                {
                    throw new Exception("Entity doesn´t exist in the database!");
                }

                restaurantRegistration.FlName = updateRestaurantRegistration.FlName;
                restaurantRegistration.Phone = updateRestaurantRegistration.Phone;
                restaurantRegistration.Email = updateRestaurantRegistration.Email;
                restaurantRegistration.Password = updateRestaurantRegistration.Password;
                restaurantRegistration.RName = updateRestaurantRegistration.RName;
                restaurantRegistration.Category = updateRestaurantRegistration.Category;
                restaurantRegistration.Desc = updateRestaurantRegistration.Desc;
                restaurantRegistration.Rphone = updateRestaurantRegistration.Rphone;
                restaurantRegistration.Location = updateRestaurantRegistration.Location;
                restaurantRegistration.Image = updateRestaurantRegistration.Image;
                restaurantRegistration.Numberoftables = updateRestaurantRegistration.Numberoftables;
                restaurantRegistration.Capacity = updateRestaurantRegistration.Capacity;
                restaurantRegistration.Openingdays = updateRestaurantRegistration.Openingdays;
                restaurantRegistration.Openinghours = updateRestaurantRegistration.Openinghours;
                restaurantRegistration.Closinghours = updateRestaurantRegistration.Closinghours;

                await _context.SaveChangesAsync();

                return restaurantRegistration;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating restaurant registration: {ex.Message}");
            }
        }

        public async Task DeleteRestaurantRegistrationAsync(int id)
        {
            RestaurantRegistrationsEfo? restaurantRegistration = await _context.RestaurantRegistrations.FirstOrDefaultAsync(
                rr => rr.Id == id);

            if (restaurantRegistration == null)
            {
                throw new Exception("Entity doesn´t exist in the database!");
            }

            _context.RestaurantRegistrations.Remove(restaurantRegistration);
            await _context.SaveChangesAsync();
        }
    }
}
