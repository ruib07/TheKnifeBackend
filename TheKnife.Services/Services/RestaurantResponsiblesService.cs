using Microsoft.EntityFrameworkCore;
using TheKnife.Entities.Efos;
using TheKnife.EntityFramework;

namespace TheKnife.Services.Services
{
    public interface IRestaurantResponsiblesService
    {
        Task<List<RestaurantResponsiblesEfo>> GetAllRestaurantResponsiblesAsync();
        Task<RestaurantResponsiblesEfo> GetRestaurantResponsibleByIdAsync(int id);
        Task<RestaurantResponsiblesEfo> SendRestaurantResponsibleAsync(RestaurantResponsiblesEfo restaurantResponsible);
        Task<RestaurantResponsiblesEfo> SendLoginRestaurantResponsibleAsync(string email, string password);
        Task<RestaurantResponsiblesEfo> UpdateRestaurantResponsibleAsync(int id, RestaurantResponsiblesEfo updateRestaurantResponsible);
        Task DeleteRestaurantResponsibleAsync(int id);
    }

    public class RestaurantResponsiblesService : IRestaurantResponsiblesService
    {
        private readonly TheKnifeDbContext _context;

        public RestaurantResponsiblesService(TheKnifeDbContext context)
        {
            _context = context;
        }

        public async Task<List<RestaurantResponsiblesEfo>> GetAllRestaurantResponsiblesAsync()
        {
            return await _context.RestaurantResponsibles.ToListAsync();
        }

        public async Task<RestaurantResponsiblesEfo> GetRestaurantResponsibleByIdAsync(int id)
        {
            RestaurantResponsiblesEfo? restaurantResponsible = await _context.RestaurantResponsibles.AsNoTracking()
                .FirstOrDefaultAsync(rr => rr.Id == id);

            if (restaurantResponsible == null)
            {
                throw new Exception("Entity doesn´t exist in the database!");
            }

            return restaurantResponsible;
        }

        public async Task<RestaurantResponsiblesEfo> SendRestaurantResponsibleAsync(RestaurantResponsiblesEfo restaurantResponsible)
        {
            await _context.RestaurantResponsibles.AddAsync(restaurantResponsible);
            await _context.SaveChangesAsync();

            return restaurantResponsible;
        }

        public async Task<RestaurantResponsiblesEfo> SendLoginRestaurantResponsibleAsync(string email, string password)
        {
            RestaurantResponsiblesEfo? loginResponsible = await _context.RestaurantResponsibles.FirstOrDefaultAsync(
                lr => lr.Email == email && lr.Password == password);

            return loginResponsible;
        }

        public async Task<RestaurantResponsiblesEfo> UpdateRestaurantResponsibleAsync(int id, RestaurantResponsiblesEfo updateRestaurantResponsible)
        {
            try
            {
                RestaurantResponsiblesEfo? restaurantResponsible = await _context.RestaurantResponsibles.FindAsync(id);

                if (restaurantResponsible == null)
                {
                    throw new Exception("Entity doesn´t exist in the database!");
                }

                restaurantResponsible.FlName = updateRestaurantResponsible.FlName;
                restaurantResponsible.Phone = updateRestaurantResponsible.Phone;
                restaurantResponsible.Email = updateRestaurantResponsible.Email;
                restaurantResponsible.Password = updateRestaurantResponsible.Password;

                await _context.SaveChangesAsync();

                return restaurantResponsible;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating restaurant responsible: {ex.Message}");
            }
        }

        public async Task DeleteRestaurantResponsibleAsync(int id)
        {
            RestaurantResponsiblesEfo? restaurantResponsible = await _context.RestaurantResponsibles.FirstOrDefaultAsync(
                rr => rr.Id == id);

            if (restaurantResponsible == null)
            {
                throw new Exception("Entity doesn´t exist in the database!");
            }

            _context.RestaurantResponsibles.Remove(restaurantResponsible);
            await _context.SaveChangesAsync();
        }
    }
}
