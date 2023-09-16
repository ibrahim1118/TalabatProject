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
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(or => or.ShaippingAddress, sh => sh.WithOwner());
            builder.Property(or => or.OrderStatus)
                .HasConversion(
                   Orderstatus => Orderstatus.ToString(),
                   os => (OrderStatus)Enum.Parse(typeof(OrderStatus), os)
                  );
            builder.Property(or => or.SubTotal).HasColumnType("decimal(18,2)"); 

        }
    }
}
