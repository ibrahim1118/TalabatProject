using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.IServices
{
    public interface ICashingService
    {
        public Task SetCashing(string key, object respons, TimeSpan liveTime); 

        public Task<string> GetCashingData (string key);    
    }
}
