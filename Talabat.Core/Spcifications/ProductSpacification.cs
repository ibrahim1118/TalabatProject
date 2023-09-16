using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entitys;
using Talabt.APIs.Helper;

namespace Talabat.Core.Spcifications
{
    public class ProductSpacification : Spacification<Product>
    {

        public ProductSpacification()
        {
            Includes.Add(p => p.productBrand);
            Includes.Add(p => p.productType);
        }
        public ProductSpacification(int id) : base(p => p.Id == id)
        {
            Includes.Add(p => p.productBrand);
            Includes.Add(p => p.productType);
        }
        public ProductSpacification(ProductSpacPrams filter) :
            base(p => (string.IsNullOrEmpty(filter.search) || p.Name.ToLower().Contains(filter.search))
                 && (!filter.BrandId.HasValue || p.ProductBrandId == filter.BrandId)
                && (!filter.Typeid.HasValue || p.ProductTypeId == filter.Typeid))
        {
            Includes.Add(p => p.productBrand);
            Includes.Add(p => p.productType);
            Skip = filter.PageSize * (filter.PageIndex - 1);
            Take = filter.PageSize;
            IsPagnation = true;

            if (!string.IsNullOrEmpty(filter.sort))
            {
                switch (filter.sort)
                {
                    case "PriceAsc":
                        OrderBy = p => p.Price;
                        break;
                    case "PriceDesc":
                        OrderByDas = p => p.Price;
                        break;
                    case "nameAsc":
                        OrderBy = p => p.Name;
                        break;
                    default:
                        OrderByDas = p => p.Name;
                        break;

                }
            }


        }
    }
}
