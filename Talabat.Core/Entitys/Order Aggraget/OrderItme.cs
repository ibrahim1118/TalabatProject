using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entitys.Order_Aggraget
{
    public class OrderItme:BaseEntity
    {
        public OrderItme()
        {

        }

        public OrderItme(ProductItmeOrder product, int count, decimal price)
        {
            Product = product;
            Count = count;
            Price = price;
        }

        public ProductItmeOrder Product { get; set; }
        public int Count { get; set; }  

        public decimal Price { get; set; }

    }
}
