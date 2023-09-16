using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.IServices;

namespace Talabat.Service
{
    public class CashingService : ICashingService
    {
        private IDatabase _database; 
        public CashingService(IConnectionMultiplexer reids)
        {
                _database =reids.GetDatabase();
        }
        public async Task<string> GetCashingData(string key)
        {
            var respons = await _database.StringGetAsync(key);
            if (respons.IsNullOrEmpty)
                return null;

            return respons; 
        }

        public async Task SetCashing(string key, object respons, TimeSpan liveTime)
        {
            if (respons == null)
                return;
            var option = new JsonSerializerOptions()
            {
                PropertyNamingPolicy= JsonNamingPolicy.CamelCase   
            };

            var serilzerRespons = JsonSerializer.Serialize(respons , option);

            await _database.StringSetAsync(key, serilzerRespons, liveTime); 
        }
    }
}
