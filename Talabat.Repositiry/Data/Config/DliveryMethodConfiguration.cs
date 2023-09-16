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
    public class DliveryMethodConfiguration : IEntityTypeConfiguration<DliveryMethod>
    {
        public void Configure(EntityTypeBuilder<DliveryMethod> builder)
        {
            builder.Property(dl => dl.Cost).HasColumnType("decimal(18,2)"); 
        }
    }
}
