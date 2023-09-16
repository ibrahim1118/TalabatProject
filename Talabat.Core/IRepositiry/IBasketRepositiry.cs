using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entitys;

namespace Talabat.Core.IRepositiry
{
    public interface IBasketRepositiry
    {
        public Task<CustomerBasket?> GetBasket(string id);

        public Task<CustomerBasket?> UpdataOrCreae(CustomerBasket customerBasket);

        public Task<bool> DeleteBasket(string id);
    }
}
