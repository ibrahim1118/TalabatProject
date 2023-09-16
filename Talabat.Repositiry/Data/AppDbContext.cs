using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Talabat.Core.Entitys;
using Talabat.Core.Entitys.Order_Aggraget;

namespace Talabat.Repositiry.Data
{
    public class AppDbContext : DbContext
    {
        

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.
                ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); 

        }

        public DbSet<Product> Products { get; set; }

        public  DbSet<ProductBrand> ProducteBrands { get; set; }  
        public  DbSet<ProductType> ProductTypes { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderItme> OrdersItmes { get;set; }

        public DbSet<DliveryMethod> DliveryMethods { get; set;}


    }
}
