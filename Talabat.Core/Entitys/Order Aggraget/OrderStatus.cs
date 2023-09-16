using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entitys.Order_Aggraget
{
    public enum OrderStatus
    {
        [EnumMember(Value ="Pending")]
        Pending,
        [EnumMember(Value = "paymentReceived")]
        paymentReceived,
        [EnumMember(Value = "paymentFieled")]

        paymentFieled
    }
}
