using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entitys;
using Talabt.APIs.Helper;

namespace Talabat.Core.Spcifications
{
    public class ProductCountWihtSpac : Spacification<Product>
    {
        public ProductCountWihtSpac(ProductSpacPrams Parms)
            : base(p =>
            string.IsNullOrEmpty(Parms.search) || p.Name.ToLower().Contains(Parms.search.ToLower()) 
            && (!Parms.BrandId.HasValue || Parms.BrandId == p.ProductBrandId)
            && (!Parms.Typeid.HasValue || Parms.Typeid == p.ProductTypeId))
        {

        }
    }
}
