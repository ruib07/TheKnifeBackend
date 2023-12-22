using Microsoft.EntityFrameworkCore;
using TheKnife.Entities.Efos;
using TheKnife.EntityFramework;

namespace TheKnife.Services.Services
{
    public interface IRestaurantsService
    {
        Task<List<RestaurantsEfo>> GetAllRestaurantsAsync();
        Task<RestaurantsEfo> GetRestaurantByIdAsync(int id);
        Task<RestaurantsEfo> SendRestaurantAsync(RestaurantsEfo restaurant);
        Task<RestaurantsEfo> UpdateRestaurantAsync(int id, RestaurantsEfo updateRestaurant);
        Task DeleteRestaurantAsync(int id);
    }

    public class RestaurantsService : IRestaurantsService
    {
        private readonly TheKnifeDbContext _context;

        public RestaurantsService(TheKnifeDbContext context)
        {
            _context = context;
        }

        public async Task<List<RestaurantsEfo>> GetAllRestaurantsAsync()
        {
            return await _context.Restaurants.ToListAsync();
        }

        public async Task<RestaurantsEfo> GetRestaurantByIdAsync(int id)
        {
            RestaurantsEfo? restaurant = await _context.Restaurants.AsNoTracking()
                .FirstOrDefaultAsync(r => r.Id == id);

            if (restaurant == null)
            {
                throw new Exception("Entity doesn´t exist in the database!");
            }

            return restaurant;
        }

        public async Task<RestaurantsEfo> SendRestaurantAsync(RestaurantsEfo restaurant)
        {
            await _context.Restaurants.AddAsync(restaurant);
            await _context.SaveChangesAsync();

            return restaurant;
        }

        public async Task<RestaurantsEfo> UpdateRestaurantAsync(int id, RestaurantsEfo updateRestaurant)
        {
            try
            {
                RestaurantsEfo? restaurant = await _context.Restaurants.FindAsync(id);

                if (restaurant == null)
                {
                    throw new Exception("Entity doesn´t exist in the database!");
                }

                restaurant.RName = updateRestaurant.RName;
                restaurant.Category = updateRestaurant.Category;
                restaurant.Desc = updateRestaurant.Desc;
                restaurant.Rphone = updateRestaurant.Rphone;
                restaurant.Location = updateRestaurant.Location;
                restaurant.Image = updateRestaurant.Image;
                restaurant.Numberoftables = updateRestaurant.Numberoftables;
                restaurant.Capacity = updateRestaurant.Capacity;
                restaurant.Openingdays = updateRestaurant.Openingdays;
                restaurant.Openinghours = updateRestaurant.Openinghours;
                restaurant.Closinghours = updateRestaurant.Closinghours;

                await _context.SaveChangesAsync();

                return restaurant;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating restaurant: {ex.Message}");
            }
        }

        public async Task DeleteRestaurantAsync(int id)
        {
            RestaurantsEfo? restaurant = await _context.Restaurants.FirstOrDefaultAsync(
                r => r.Id == id);

            if (restaurant == null )
            {
                throw new Exception("Entity doesn´t exist in the database!");
            }

            _context.Restaurants.Remove(restaurant);
            await _context.SaveChangesAsync();
        }
    }
}
