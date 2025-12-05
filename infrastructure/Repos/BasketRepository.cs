using Domain.Contracts;
using Domain.Entities;
using StackExchange.Redis;
using System.Text.Json;

namespace infrastructure.Repos
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase redisDb;
        public BasketRepository(IConnectionMultiplexer _redis)
        {
            redisDb = _redis.GetDatabase();
        }
        public async Task<bool> DeleteBasketAsync(string basketId)
        {
            return await redisDb.KeyDeleteAsync(basketId);

        }

        public async Task<CustomerBasket?> GetBasketAsync(string basketId)
        {

            var basket = await redisDb.StringGetAsync(basketId);
            return basket.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(basket!);


        }

        public async Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket)
        {
            var serializedBasket = JsonSerializer.Serialize(basket);
            var created = await redisDb.StringSetAsync(basket.Id, serializedBasket, TimeSpan.FromDays(30));
            if (created is false)
                return null;
            return await GetBasketAsync(basket.Id) ?? null;
        }
    }
}
