using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Talabat.Core.Entitys;
using Talabat.Core.IRepositiry;

namespace Talabat.Repositiry.Repositiry
{
    public class BasketRepositiry : IBasketRepositiry
    {
        private IDatabase _database;
        public BasketRepositiry(IConnectionMultiplexer redis)
        {
            _database =redis.GetDatabase();
        }
        
        public async Task<bool> DeleteBasket(string id)
        {
            return await _database.KeyDeleteAsync(id);
        }

        public async Task<CustomerBasket?> GetBasket(string id)
        {
            var Basket = await _database.StringGetAsync(id);
            return Basket.IsNull ? null : JsonSerializer.Deserialize<CustomerBasket>(Basket);
        }

        public async Task<CustomerBasket?> UpdataOrCreae(CustomerBasket customerBasket)
        {
            var res = await _database.StringSetAsync(customerBasket.Id, JsonSerializer.Serialize(customerBasket), TimeSpan.FromDays(2));

            return !res ? null : await GetBasket(customerBasket.Id); 
        }
    }
}
