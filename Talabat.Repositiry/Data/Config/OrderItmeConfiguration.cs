using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entitys.Order_Aggraget;

namespace Talabat.Repositiry.Data.Config
{
    public class OrderItmeConfiguration : IEntityTypeConfiguration<OrderItme>
    {
        public void Configure(EntityTypeBuilder<OrderItme> builder)
        {
            builder.OwnsOne(or => or.Product, p => p.WithOwner());

            builder.Property(or => or.Price).HasColumnType("decimal(18,2)"); 
        }

    }
}
