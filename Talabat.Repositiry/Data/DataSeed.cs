using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entitys;
using Talabat.Core.Entitys.Order_Aggraget;

namespace Talabat.Repositiry.Data
{
    public static class DataSeed
    {
        public static async Task SeedingData(AppDbContext context)
        {
            if (!context.ProducteBrands.Any())
            {
                var BrandData = File.ReadAllText("D:\\project\\Talabt.APIs\\Talabat.Repositiry\\Data\\DataSeed\\brands.json");
                var Brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandData);

                if (Brands?.Count > 0)
                {
                    foreach (var type in Brands)
                    {
                        var t = new ProductBrand() { Name = type.Name };

                        await context.ProducteBrands.AddAsync(t);
                    }
                    await context.SaveChangesAsync();
                }
            }
        if (!context.ProductTypes.Any())
          {
            var BrandData = File.ReadAllText("D:\\project\\Talabt.APIs\\Talabat.Repositiry\\Data\\DataSeed\\types.json");
            var Types = JsonSerializer.Deserialize<List<ProductType>>(BrandData);

              if (Types?.Count > 0)
              {
                  foreach (var type in Types)
                  {
                      var t = new ProductType() { Name = type.Name };

                  await context.ProductTypes.AddAsync(t);
                  }
                  await context.SaveChangesAsync();
                }
                }
            if (!context.Products.Any())
            {
                var ProudctData = File.ReadAllText("D:\\project\\Talabt.APIs\\Talabat.Repositiry\\Data\\DataSeed\\products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(ProudctData);

                if (products?.Count > 0)
                {
                    foreach (var  product in products)
                    {
                      
                        await context.Products.AddAsync(product);
                    }
                    await context.SaveChangesAsync();
                }
            }

            if (!context.DliveryMethods.Any())
            {

                var text = File.ReadAllText("D:\\project\\Talabt.APIs\\Talabat.Repositiry\\Data\\DataSeed\\delivery.json"); 
                var DliverMetod = JsonSerializer.Deserialize<List<DliveryMethod>>(text);
                foreach (var method in DliverMetod)
                {
                    await context.DliveryMethods.AddAsync(method); 
                }
                 await  context.SaveChangesAsync();
            }

        }
    }
}
