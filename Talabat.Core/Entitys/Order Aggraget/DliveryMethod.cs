using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entitys.Order_Aggraget
{
    public class DliveryMethod : BaseEntity
    {
        public DliveryMethod()
        {

        }
        public DliveryMethod(string shortName, string description, string dliverTime, decimal cost)
        {
            ShortName = shortName;
            Description = description;
            DliverTime = dliverTime;
            Cost = cost;
        }

        public string ShortName { get; set; }

        public string Description { get; set; }

        public string DliverTime { get; set; }

        public  decimal Cost { get; set; }  
    }
}
